namespace PropertyManagementService.Services.Test.Tests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Domain.Infrastructure.Enum;
    using System;
    using System.Linq;
    using Xunit;

    public class UtilityServiceTests
    {
        public UtilityServiceTests()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void CreateShouldAddNewUtilityToDatabase()
        {
            //Arrange
            var db = this.GetContext();

            var utilities = new UtilityService(db);

            var buildingId = 1;
            var managerId = "manId";

            db.Buildings.Add(new Building
            {
                Id = buildingId,
                ManagerId = managerId
            });

            var utility = new BuildingUtility()
            {
                Name = "CableTv",
                Description = "CableTV Monthly Fee",
                Price = 25.99M,
                IsPerResident = true,
                IsSubscribable = false,
                Routine = Routine.Monthly,
                BuildingId = buildingId
            };

            //Act
            utilities.Create(managerId, utility.Name, utility.Description, utility.Price, utility.IsSubscribable, utility.Routine, utility.IsPerResident, utility.BuildingId);

            //Assert
            db.BuildingUtilities.FirstOrDefault().Name.Should().Be(utility.Name);
            db.BuildingUtilities.FirstOrDefault().Description.Should().Be(utility.Description);
            db.BuildingUtilities.FirstOrDefault().Price.Should().Be(utility.Price);
            db.BuildingUtilities.FirstOrDefault().IsSubscribable.Should().Be(utility.IsSubscribable);
            db.BuildingUtilities.FirstOrDefault().IsPerResident.Should().Be(utility.IsPerResident);
            db.BuildingUtilities.FirstOrDefault().BuildingId.Should().Be(utility.BuildingId);
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
