namespace PropertyManagementService.Services
{
    using Contracts;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Infrastructure;
    using System;
    using System.Linq;

    public class ApartmentService : IApartmentService
    {
        private readonly PropertyManagementServiceDbContext db;

        public ApartmentService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public void Create(string number, int residents, int dogs, int area, string ownerId, int buildingId)
        {
            if (!this.db.Buildings.Any(b => b.Id == buildingId) || !this.db.Users.Any(u => u.Id == ownerId))
            {
                throw new ArgumentException(Constants.UserOrBuildingDoNotExistError);
            }

            if (!this.db.Buildings
                .Where(b => b.Id == buildingId)
                .Select(b => b.Apartments.Any(a => a.Number == number))
                .SingleOrDefault())
            {
                this.db.Apartments.Add(new Apartment
                {
                    Number = number,
                    Residents = residents,
                    Dogs = dogs,
                    Area = area,
                    OwnerId = ownerId,
                    BuildingId = buildingId
                });

                this.db.SaveChanges();
            }
            else
            {
                throw new ArgumentException(Constants.ApartmentDoesNotExistError);
            }
        }
    }
}
