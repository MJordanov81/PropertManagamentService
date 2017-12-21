namespace PropertyManagementService.Services.Test.Tests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTests
    {
        public UserServiceTests()
        {
            TestStartup.Initialize();
        }

        [Fact]
        public void EditUserShouldEditUSerRoles()
        {
            //Arrange
            var db = this.GetContext();

            var users = new UserService(db);

            var roleName = "Administrator";
            var userId = "userId";

            db.Users.Add(new User
            {
                Id = userId,
            });

            db.SaveChanges();

            db.UserRoleNames.Add(new UserRoleName
            {
                UserId = userId,
                RoleName = roleName
            });

            //Act

            users.EditUser(userId, new List<string> { "Manager" });

            //Assert

            User user = db.Users.Find(userId);

            user.RolesNames.Should().HaveCount(2);
            user.RolesNames.ToList()[1].RoleName.Should().Be("Manager");
        }

        [Fact]
        public async Task AddRoleNameToUserShouldAddProvidedRoleToUser()
        {
            //Arrange
            var db = this.GetContext();

            var users = new UserService(db);

            var roleName = "Administrator";
            var userId = "userId";

            db.Users.Add(new User
            {
                Id = userId
            });

            db.SaveChanges();

            //Act

            await users.AddRoleNameToUser(userId, roleName);

            //Assert

            User user = await db.Users.FindAsync(userId);

            user.RolesNames.FirstOrDefault().RoleName.Should().Be(roleName);

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
