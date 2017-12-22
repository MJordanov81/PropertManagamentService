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

        public void Create(string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId, string imageUrl)
        {
            this.db.Add(new Building
            {
                Contract = contract,
                Address = address,
                ServiceStartDate = serviceStartDate,
                ServiceEndDate = serviceEndDate,
                ManagerId = managerId,
                ImageUrl = imageUrl
            });

            db.SaveChanges();
        }

        public void EditBuilding(int buildingId, string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId, string imageUrl)
        {
            Building building = this.db.Buildings.Find(buildingId);

            building.Contract = contract;
            building.Address = address;
            building.ServiceStartDate = serviceStartDate;
            building.ServiceEndDate = serviceEndDate;
            building.ManagerId = managerId;
            building.ImageUrl = imageUrl;

            this.db.SaveChanges();
        }

        public BuildingManagerDetailsModel GetBuildingDetails(int buildingId)
        {
            if (!this.db.Buildings.Any(b => b.Id == buildingId))
            {
                return null;
            }

            return this.db.Buildings
                .Where(b => b.Id == buildingId)
                .ProjectTo<BuildingManagerDetailsModel>()
                .SingleOrDefault();
        }

        public BuildingsPaginatedModel<TModel> GetBuildings<TModel>(string search, string managerId = null)
        {
            Func<string, string, bool> searchPredicate = (address, contract)
                => address.ToLower().Contains(search.ToLower()) ||
                        contract.ToLower().Contains(search.ToLower());

            BuildingsPaginatedModel<TModel> result = new BuildingsPaginatedModel<TModel>
            {
                ItemsCount = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract)).Count(),
                Buildings = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract))
                .ProjectTo<TModel>()
                .ToList(),
            };

            if (managerId != null)
            {
                return new BuildingsPaginatedModel<TModel>
                {
                    ItemsCount = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract)).Count(),
                    Buildings = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract) && b.ManagerId == managerId)
                        .ProjectTo<TModel>()
                        .ToList(),
                };
            }
            else
            {
                return new BuildingsPaginatedModel<TModel>
                {
                    ItemsCount = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract)).Count(),
                    Buildings = this.db.Buildings.Where(b => searchPredicate(b.Address, b.Contract))
                        .ProjectTo<TModel>()
                        .ToList(),
                };
            }
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
