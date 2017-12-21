namespace PropertyManagementService.Services.Test.Tests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Linq;
    using Xunit;

    public class BuildingServiceTests
    {
        public BuildingServiceTests()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void EditBuildingShouldEditBuildingDetails()
        {
            //Arrange
            var db = this.GetContext();

            var buildings = new BuildingService(db);

            var buildingOriginalAddress = "Sofia, Lulin 4, bl. 404";

            var buildingUpdatedAddress = "Mladost 2, bl 202";

            //Act

            buildings.Create("C12", buildingOriginalAddress, new DateTime(2018, 1, 1), null, "id_1", "imageUrl");

            Building building = db.Buildings.FirstOrDefault();

            int buildingId = building.Id;

            db.SaveChanges();

            buildings.EditBuilding(buildingId, "C12", buildingUpdatedAddress, new DateTime(2018, 1, 1), null, "id_1", "imageUrl");

            //Assert
            building = db.Buildings.FirstOrDefault();

            building.Address.Should().Be(buildingUpdatedAddress);
        }

        [Fact]
        public void CreateShouldAddBuildingToDatabase()
        {
            //Arrange
            var db = this.GetContext();

            var buildings = new BuildingService(db);

            //Act

            buildings.Create("C12", "Sofia, Lulin 4, bl. 404", new DateTime(2018, 1, 1), null, "id_1", "imageUrl");

            //Assert
            int buildingCount = db.Buildings.Count();

            Assert.Equal(1, buildingCount);
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
