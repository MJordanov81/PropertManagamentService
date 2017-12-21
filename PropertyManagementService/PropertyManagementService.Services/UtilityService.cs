namespace PropertyManagementService.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Utility;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Domain.Infrastructure.Enum;
    using System;
    using System.Linq;

    public class UtilityService : IUtilityService
    {
        private readonly PropertyManagementServiceDbContext db;

        public UtilityService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public UtilitiesPaginatedModel GetUtilitiesForBuilding(int buildingId, string search)
        {

            Func<string, bool> searchPredicate = (name)
                 => name.ToLower().Contains(search.ToLower());

            return this.db.Buildings.Where(b => b.Id == buildingId).Select(b => new UtilitiesPaginatedModel
            {
                BuildingId = b.Id,
                ItemsCount = b.Utilities.Where(bu => searchPredicate(bu.Name)).Count(),
                Utilities = b.Utilities.Where(bu => searchPredicate(bu.Name))
                .AsQueryable()
                .ProjectTo<UtilityBuildingListModel>()
                .ToList(),

            })
            .SingleOrDefault();
        }

        public void Create(string currentUserId, string name, string description, decimal price, bool isSubscribable, Routine routine, bool isPerResident, int buildingId)
        {
            if (this.db.BuildingUtilities.Any(u => u.Name == name && u.BuildingId == buildingId))
            {
                throw new ArgumentException($"Utility with name {name} already exists.");
            }
            if (this.db.Buildings.Find(buildingId).ManagerId != currentUserId)
            {
                throw new InvalidOperationException($"No Authorization!");
            }

            this.db.BuildingUtilities.Add(new BuildingUtility
            {
                Name = name,
                Description = description,
                Price = price,
                IsSubscribable = isSubscribable,
                Routine = routine,
                IsPerResident = isPerResident,
                BuildingId = buildingId
            });

            this.db.SaveChanges();
        }
    }
}
