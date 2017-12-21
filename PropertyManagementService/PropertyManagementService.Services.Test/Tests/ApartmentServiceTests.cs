namespace PropertyManagementService.Services.Test.Tests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Linq;
    using Xunit;

    public class ApartmentServiceTests
    {
        [Fact]
        public void CreateShouldThrowExceptonIfUserDoesNotExist()
        {
            //Arrange

            var db = GetContext();

            var apartments = new ApartmentService(db);

            db.Buildings.Add(new Building
            {
                Id = 1
            });

            db.SaveChanges();

            //Assert
       
            Exception ex = Assert.Throws<ArgumentException>(() => apartments.Create("A1", 4, 1, 120, "id_1", 1));

            ex.Message.Should().Be("User or building with given ids do not exist.");
        }

        [Fact]
        public void CreateShouldThrowExceptionIfApartmentNumberIsDuplicated()
        {
            //Arrange

            var db = GetContext();

            var apartments = new ApartmentService(db);

            db.Users.Add(new User
            {
                Id = "id_1"
            });

            db.Buildings.Add(new Building
            {
                Id = 1
            });

            db.SaveChanges();

            //Act

            apartments.Create("A1", 4, 1, 120, "id_1", 1);

            //Asert

            Exception ex = Assert.Throws<ArgumentException>(() => apartments.Create("A1", 4, 1, 120, "id_1", 1));

            ex.Message.Should().Be("Apartment with the given number already exists.");
        }

        [Fact]
        public void CreateShouldAddANewApartmentToDatabase()
        {
            //Arrange

            var db = GetContext();

            var apartments = new ApartmentService(db);

            db.Users.Add(new User
            {
                Id = "id_1"
            });

            db.Buildings.Add(new Building
            {
                Id = 1
            });

            db.SaveChanges();

            //Act

            apartments.Create("A1", 4, 1, 120, "id_1", 1);

            //Asert

            int numberOfApartments = db.Apartments.Count();

            numberOfApartments.Should().Be(1);
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
