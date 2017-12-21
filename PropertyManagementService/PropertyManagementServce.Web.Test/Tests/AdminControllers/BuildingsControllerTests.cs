namespace PropertyManagementService.Web.Test.Tests.AdminControllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services;
    using PropertyManagementService.Services.Models.Building;
    using PropertyManagementService.Web.Areas.Admin.Controllers;
    using PropertyManagementService.Web.Areas.Admin.Models.Buildings;
    using System;
    using Xunit;

    public class BuildingControllersTest
    {
        public BuildingControllersTest()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void EditShouldAlterBuildingDataInDatabase()
        {
            //Arrange
            var db = this.GetContext();

            var controller = this.GetController(db);

            var buildingId = 1;

            db.Buildings.Add(new Building
            {
                Id = buildingId,
                Contract = "C12",
                Address = "Sofia",
                ServiceStartDate = new DateTime(2018, 1, 1),
                ServiceEndDate = null,
                ManagerId = "manId",
                ImageUrl = "imageUrl"
            });

            db.SaveChanges();

            var updatedBuilding = new BuildingModifyDataModel
            {
                Contract = "C13",
                Address = "Plovdiv",
                ServiceStartDate = new DateTime(2018, 2, 1),
                ServiceEndDate = null,
                ManagerId = "manId",
                ImageUrl = "imageUrl"
            };

            //Act

            controller.Edit(buildingId, updatedBuilding);

            //Assert

            Func<string, string, bool> isBuildingUpdated = (contract, address) => contract == "C13" && address == "Plovdiv";

            db.Buildings.Should().OnlyContain(b => isBuildingUpdated(b.Contract, b.Address));
        }

        [Fact]
        public void CreatehouldReturnViewResultWithBuildingCreateViewModel()
        {
            //Arrange
            var db = this.GetContext();

            var controller = this.GetController(db);

            //Act

            var result = controller.Create();

            //Assert

            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<BuildingCreateViewModel>();

        }

        private BuildingsController GetController(PropertyManagementServiceDbContext db)
        {
            var users = new UserService(db);

            var buildings = new BuildingService(db);

            var controller = new BuildingsController(users, buildings);

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
