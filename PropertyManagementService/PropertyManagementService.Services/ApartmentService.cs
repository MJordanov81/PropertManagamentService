namespace PropertyManagementService.Services
{
    using Contracts;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
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
                throw new ArgumentException("User or building with given ids do not exist.");
            }

            if (!this.db.Buildings
                .Where(b => b.Id == buildingId)
                .Select(b => b.Apartments.Any(a => a.Number == number))
                .FirstOrDefault())
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
                throw new ArgumentException("Apartment with the given number already exists.");
            }
        }
    }
}
