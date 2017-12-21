namespace PropertyManagementService.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Bill;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Domain.Infrastructure.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BillService : IBillService
    {
        private readonly PropertyManagementServiceDbContext db;

        public BillService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public BillsForBuildingModel GetBillsForBuilding(string search, int buildingId, bool isConfirmed = true)
        {
            Func<string, bool> searchPredicate = (appartmentNumber)
                => appartmentNumber.ToLower().Contains(search.ToLower());

            Building building = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .SingleOrDefault();

            if (building == null)
            {
                throw new ArgumentException($"Cannot find building with id {buildingId}");
            }

            BillsForBuildingModel mappedBuilding = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .ProjectTo<BillsForBuildingModel>()
                .FirstOrDefault();

            mappedBuilding.Bills = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .SelectMany(b => b.Apartments.SelectMany(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed == isConfirmed && searchPredicate(bill.Apartment.Number))))
                .OrderBy(bill => bill.DueDate).AsQueryable().ProjectTo<BillManagerListModel>().ToList();

            mappedBuilding.ItemsCount = mappedBuilding.Bills.Count;

            if (search != string.Empty)
            {
                mappedBuilding.UnpaidBillsAmount = mappedBuilding.Bills.Where(b => searchPredicate(b.Apartment)).Sum(b => b.TotalAmount);

                mappedBuilding.UnpaidBills = mappedBuilding.Bills.Where(b => searchPredicate(b.Apartment)).Count();
            }

            return mappedBuilding;
        }

        public void GenerateBills(string currentUserId, int buildingId, int period, int year)
        {
            string buildingManagerId = this.db.Buildings.Find(buildingId).ManagerId;

            if (currentUserId != buildingManagerId)
            {
                throw new InvalidOperationException("No authorization!");
            }

            //Get all apartments without a bill(either confirmed or not) for the given period
            IList<int> apartmentsWithoutBills = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .Select(b => b.Apartments.Where(a => !a.Bills.Any(bill => bill.Period == period && bill.Year == year)).Select(a => a.Id))
                .FirstOrDefault()
                .ToList();

            //Get all utilities for the given building
            IList<int> utilities = new List<int>();

            //If period = 0 get utilities paid yearly or else those paid on monthly basis
            if (period == 0)
            {
                utilities = this.db.BuildingUtilities
                .Where(u => u.BuildingId == buildingId && u.Routine == Routine.Yearly)
                .Select(u => u.Id)
                .ToList();
            }
            else
            {
                utilities = this.db.BuildingUtilities
                .Where(u => u.BuildingId == buildingId && u.Routine == Routine.Monthly)
                .Select(u => u.Id)
                .ToList();
            }

            if (apartmentsWithoutBills.Count < 1)
            {
                throw new ArgumentException("All bills have been invoiced");
            }
            if (utilities.Count < 1)
            {
                throw new ArgumentException("There are no utilities to invoice.");
            }

            foreach (var apartmentId in apartmentsWithoutBills)
            {
                //Apartment
                Apartment apartment = this.db.Apartments.Find(apartmentId);

                Bill bill = new Bill
                {
                    Year = year,
                    Period = period,
                    DueDate = DateTime.Now.AddDays(30),
                    ApartmentId = apartmentId,
                    TotalAmount = 0

                };

                this.db.Bills.Add(bill);

                this.db.SaveChanges();

                decimal totalAmount = 0;

                foreach (var utilityId in utilities)
                {
                    //Calculate only if utility is not unsubscribed
                    if (!this.db.UnsubscribedUtilities.Any(u => u.BuildingUtilityId == utilityId && u.ApartmentId == apartmentId))
                    {
                        //Utility
                        BuildingUtility utility = this.db.BuildingUtilities.Find(utilityId);

                        BillUtility billedUtility = new BillUtility
                        {
                            BillId = bill.Id,
                            BuildingUtilityId = utilityId
                        };

                        this.db.BillUtilities.Add(billedUtility);

                        this.db.SaveChanges();

                        if (utility.IsPerResident)
                        {
                            totalAmount += utility.Price * apartment.Residents;
                        }
                        else
                        {
                            totalAmount += utility.Price;
                        }
                    }
                }

                bill.TotalAmount = totalAmount;

                this.db.SaveChanges();
            }
        }

        public async Task<int> DeleteMultiple(string currentUserId, int[] billsIds)
        {
            string buildingManagerId = this.db.Bills
                .Where(b => b.Id == billsIds[0])
                .Select(b => b.Apartment.Building.ManagerId)
                .FirstOrDefault();

            if (currentUserId != buildingManagerId)
            {
                throw new InvalidOperationException("No authorization!");
            }

            int deletedBills = 0;

            foreach (var billId in billsIds)
            {
                Bill bill = this.db.Bills.Find(billId);

                if (bill != null)
                {

                    BillUtility[] billUtilities = this.db.BillUtilities.Where(bu => bu.BillId == billId).ToArray();

                    this.db.BillUtilities.RemoveRange(billUtilities);

                    await this.db.SaveChangesAsync();

                    this.db.Bills.Remove(this.db.Bills.Find(billId));

                    await this.db.SaveChangesAsync();

                    deletedBills++;
                }
            }

            return deletedBills;
        }

        public async Task<int> ConfirmMultiple(string currentUserId, int[] billsIds)
        {
            string buildingManagerId = this.db.Bills
                .Where(b => b.Id == billsIds[0])
                .Select(b => b.Apartment.Building.ManagerId)
                .FirstOrDefault();

            if (currentUserId != buildingManagerId)
            {
                throw new InvalidOperationException("No authorization!");
            }

            int confirmedBills = 0;

            foreach (var billId in billsIds)
            {
                Bill bill = this.db.Bills.Find(billId);

                if (bill != null)
                {
                    bill.IsConfirmed = true;

                    confirmedBills++;
                }
            }

            await this.db.SaveChangesAsync();

            return confirmedBills;
        }
    }
}
