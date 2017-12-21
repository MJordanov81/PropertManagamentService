namespace PropertyManagementService.Web.Test.Tests.ManagerControllers
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services;
    using PropertyManagementService.Web.Areas.Manager.Controllers;
    using PropertyManagementService.Web.Areas.Manager.Models.Apartments;
    using System;
    using Xunit;

    public class ApartmentsControllerTests
    {
        public ApartmentsControllerTests()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void CreateShouldAddNewApartmentToDatabase()
        {
            //Arrange
            var db = this.GetContext();

            var controller = this.GetController(db);

            var ownerId = "userId";
            var buildingId = 1;

            db.Users.Add(new User
            {
                Id = ownerId
            });

            db.Buildings.Add(new Building
            {
                Id = buildingId
            });

            db.SaveChanges();

            var apartment = new ApartmentCreateViewModel
            {
                Number = "A101",
                Residents = 4,
                Dogs = 1,
                Area = 120,
                OwnerId = ownerId,
                BuildingId = buildingId
            };

            //Act
            controller.Create(apartment);

            //Assert
            db.Apartments.Should().ContainSingle().And.OnlyContain(a => a.Number == "A101");
        }

        private ApartmentsController GetController(PropertyManagementServiceDbContext db)
        {
            var users = new UserService(db);

            var apartments = new ApartmentService(db);

            var controller = new ApartmentsController(users, apartments);

            return controller;
        }

        private PropertyManagementServiceDbContext GetContext()
        {
            var dbOptions = new DbContextOptionsBuilder<PropertyManagementServiceDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return new PropertyManagementServiceDbContext(dbOptions);
        }
    }
}
