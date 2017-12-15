namespace PropertyManagementService.Services
{
    using Contracts;
    using Models.User;
    using PropertyManagementService.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly PropertyManagementServiceDbContext db;

        public UserService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserEmailModel> GetUsersEmailsList(string roleName = null)
        {
            string roleId = this.db.Roles
                .Where(r => r.Name == roleName)
                .Select(r => r.Id)
                .FirstOrDefault();

            var users = this.db.Users.Select(u => new
            {
                Id = u.Id,
                Email = u.Email,
                RoleId = this.db.UserRoles
                .Where(ur => ur.UserId == u.Id)
                .Select(ur => ur.RoleId)
                .FirstOrDefault()
            })
            .ToList();

            if (roleName != null)
            {
                users = users.Where(u => u.RoleId == roleId).ToList();
            }

            return users.Select(u => new UserEmailModel
            {
                Id = u.Id,
                Email = u.Email
            });
        }
    }
}
