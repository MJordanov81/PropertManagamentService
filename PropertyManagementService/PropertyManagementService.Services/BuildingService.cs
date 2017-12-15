namespace PropertyManagementService.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Building;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Linq;

    public class BuildingService : IBuildingService
    {
        private readonly PropertyManagementServiceDbContext db;

        public BuildingService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public void Create(string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId)
        {
            this.db.Add(new Building
            {
                Contract = contract,
                Address = address,
                ServiceStartDate = serviceStartDate,
                ManagerId = managerId
            });

            db.SaveChanges();
        }

        public void EditBuilding(int buildingId, string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId)
        {
            Building building = this.db.Buildings.Find(buildingId);

            building.Contract = contract;
            building.Address = address;
            building.ServiceStartDate = serviceStartDate;
            building.ServiceEndDate = serviceEndDate;
            building.ManagerId = managerId;

            this.db.SaveChanges();
        }

        public BuildingsAdminPaginatedModel GetBuildings(string search)
        {
            Func<string, string, bool> searchPredicate = (address, contract)
                => address.ToLower().Contains(search.ToLower()) ||
                        contract.ToLower().Contains(search.ToLower());

            return new BuildingsAdminPaginatedModel
            {
                ItemsCount = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract)).Count(),
                Buildings = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract))
                .ProjectTo<BuildingAdminListModel>()
                .ToList(),
            };
        }

        public BuildingModifyDataModel GetBuildingToEdit(int buildingId)
        {
            if (!this.db.Buildings.Any(b => b.Id == buildingId))
            {
                return null;
            }

            return this.db.Buildings
                .Where(b => b.Id == buildingId)
                .ProjectTo<BuildingModifyDataModel>()
                .SingleOrDefault();
        }
    }
}
