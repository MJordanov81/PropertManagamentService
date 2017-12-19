namespace PropertyManagementService.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Bill;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BillService : IBillService
    {
        private readonly PropertyManagementServiceDbContext db;

        public BillService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public BillsForBuildingModel GetBillsForBuilding(int buildingId, bool isConfirmed = true)
        {
            Building building =  this.db.Buildings
                .Where(b => b.Id == buildingId)
                .SingleOrDefault();

            if (building == null)
            {
                throw new ArgumentException($"Cannot find building with id {building}");
            }

            BillsForBuildingModel mappedBuilding = Mapper.Map<BillsForBuildingModel>(building);

            var billss = this.db.Buildings.Where(b => b.Id == buildingId).SelectMany(b => b.Apartments.SelectMany(a => a.Bills)).ToList();

            mappedBuilding.Bills = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .SelectMany(b => b.Apartments.SelectMany(a => a.Bills.Where(bill => !bill.IsPaid && bill.IsConfirmed == isConfirmed)))
                .OrderBy(bill => bill.DueDate).AsQueryable().ProjectTo<BillManagerListModel>().ToList();

            return mappedBuilding;
        }

        public void GenerateBills(int buildingId, int period, int year)
        {
            IList<int> apartmentsWithoutBills = this.db.Buildings
                .Where(b => b.Id == buildingId)
                .Select(b => b.Apartments.Where(a => !a.Bills.Any(bill => bill.Period == period && bill.Year == year)).Select(a => a.Id))
                .FirstOrDefault()
                .ToList();

            IList<int> utilities = this.db.BuildingUtilities
                .Where(u => u.BuildingId == buildingId)
                .Select(u => u.Id)
                .ToList();

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
                    if (!this.db.UnsubscribedUtilities.Any(u => u.BuildingUtilityId == utilityId && u.ApartmentId == apartmentId))
                    {
                        BillUtility billedUtility = new BillUtility
                        {
                            BillId = bill.Id,
                            BuildingUtilityId = utilityId
                        };

                        this.db.BillUtilities.Add(billedUtility);

                        this.db.SaveChanges();

                        totalAmount += this.db.BuildingUtilities.Find(utilityId).Price;
                    }
                }

                bill.TotalAmount = totalAmount;

                this.db.SaveChanges();
            }
        }
    }
}
