namespace PropertyManagementService.Services.Test.Tests
{
    using Xunit;
    using FluentAssertions;
    using PropertyManagementService.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Models.Bill;
    using System.Threading.Tasks;
    using System.Linq;

    public class BillServiceTests
    {
        public BillServiceTests()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void GetBillsForBuildingShouldReturnBillsForBuildingModelResultWithZeroBills()
        {
            //Arrange
            var db = this.GetContext();

            var bills = new BillService(db);

            var buildingId = 1;

            //Act
            db.Buildings.Add(new Building
            {
                Id = buildingId
            });

            db.SaveChanges();

            //Assert
            var result = bills.GetBillsForBuilding("", buildingId);

            result.Should().BeOfType<BillsForBuildingModel>();

            result.Bills.Count.Should().Be(0);
        }

        [Fact]
        public void GetBillsForBuildingShouldThrowExceptionIfBuildingDoesNotExist()
        {
            //Arrange
            var db = this.GetContext();

            var bills = new BillService(db);

            var buildingId = 1;

            //Assert
            Exception ex = Assert.Throws<ArgumentException>(() => bills.GetBillsForBuilding(null, buildingId));

            ex.Message.Should().Be($"Cannot find building with id {buildingId}");
        }

        [Fact]
        public async Task DeleteMultipleShouldDeleteBillsWithGivenIds()
        {
            //Arange
            var db = this.GetContext();

            var bills = new BillService(db);

            var billsIds = new int[] { 1, 2, 3, 4 };

            foreach (var billId in billsIds)
            {
                await db.Bills.AddAsync(new Bill { Id = billId, ApartmentId = 1 });
            }
          
            await db.Buildings.AddAsync(new Building
            {
                ManagerId = "currentUserId",
                Id = 1
            });

            await db.SaveChangesAsync();

            await db.Apartments.AddAsync(new Apartment
            {
                BuildingId = 1,
                Id = 1,
                Number = "A1"

            });

            await db.SaveChangesAsync();

            int billsCount = db.Bills.Count();

            //Act

            int deletedBills = await bills.DeleteMultiple("currentUserId", billsIds);

            //Assert

            billsCount.Should().Be(billsIds.Length);

            deletedBills.Should().Be(billsIds.Length);

            db.Bills.Count().Should().Be(0);

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
