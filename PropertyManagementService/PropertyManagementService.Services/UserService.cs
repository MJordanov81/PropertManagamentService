namespace PropertyManagementService.Services
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.User;
    using PropertyManagementService.Data;
    using PropertyManagementService.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly PropertyManagementServiceDbContext db;

        public UserService(PropertyManagementServiceDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserEmailModel> GetUsersEmailsList(string roleName = null)
        {
            var users = this.db.Users.Select(u => new
            {
                Id = u.Id,
                Email = u.Email,
                RolesNames = u.RolesNames
            })
            .ToList();

            if (roleName != null)
            {
                users = users.Where(u => u.RolesNames.Any(rn => rn.RoleName == roleName)).ToList();
            }

            return users.Select(u => new UserEmailModel
            {
                Id = u.Id,
                Email = u.Email
            });
        }

        public UsersAdminPaginatedModel GetUsers(string search, IList<string> rolesSearch)
        {
            Func<string, string, bool> searchPredicate = (email, name)
                => email.ToLower().Contains(search.ToLower()) ||
                        name.ToLower().Contains(search.ToLower());

            bool isRoleSearchEmpty = rolesSearch.Count == 0 ? true : false;

            Dictionary<string, bool> roles = this.db.Roles
                .Select(r => new { Key = r.Name, Value = isRoleSearchEmpty })
                .ToDictionary(r => r.Key, r => r.Value);

            if (!isRoleSearchEmpty)
            {
                for (int i = 0; i < rolesSearch.Count; i++)
                {
                    if (roles.ContainsKey(rolesSearch[i]))
                    {
                        roles[rolesSearch[i]] = true;
                    }
                }
            }

            UsersAdminPaginatedModel users = new UsersAdminPaginatedModel
            {
                ItemsCount = this.db.Users.Where(b => searchPredicate(b.UserName, b.Name)).Count(),
                Users = this.db.Users.Where(b => searchPredicate(b.UserName, b.Name))
                .ProjectTo<UserAdminListModel>()
                .Where(u => u.Roles.Any(r => roles.Where(rr => rr.Value == true).Select(rr => rr.Key).Contains(r)))
                .OrderBy(u => u.Name)
                .ToList(),
                IncludedRoles = roles
            };

            return users;

        }

        public async Task AddRoleNameToUser(string userId, string roleName)
        {
            if (this.db.Users.Any(u => u.Id == userId) && !this.db.UserRoleNames.Any(ur => ur.UserId == userId && ur.RoleName == roleName))
            {
                await this.db.UserRoleNames.AddAsync(new UserRoleName
                {
                    UserId = userId,
                    RoleName = roleName
                });

                await this.db.SaveChangesAsync();
            }
        }

        public UserModifyDataModel GetUserToEdit(string userId)
        {
            if (!this.db.Users.Any(u => u.Id == userId))
            {
                return null;
            }

            return this.db.Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserModifyDataModel>()
                .SingleOrDefault();
        }

        public IEnumerable<string> GetRolesNames()
        {
            return this.db.Roles
                .Select(r => r.Name)
                .ToList();
        }

        public void EditUser(string userId, ICollection<string> userNewRoles)
        {
            UserRoleName[] userRoles = this.db.UserRoleNames
                .Where(u => u.UserId == userId)
                .ToArray();

            ICollection<string> userCurrentRoles = this.db.UserRoleNames
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleName)
                .ToList();

            foreach (var role in userCurrentRoles)
            {
                if (!userNewRoles.Contains(role))
                {
                    this.db.UserRoleNames
                        .Remove(userRoles.FirstOrDefault(u => u.RoleName == role));

                    this.db.SaveChanges();
                }
            }

            foreach (var role in userNewRoles)
            {
                if (!userCurrentRoles.Contains(role))
                {
                    this.db.UserRoleNames.Add(new UserRoleName
                    {
                        UserId = userId,
                        RoleName = role
                    });

                    this.db.SaveChanges();
                }
            }
        }

        public User GetUser(string userId)
        {
            return this.db.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<OwnerNameModel> GetOwners()
        {
            return this.db.Users
                .Where(u => u.RolesNames.Any(r => r.RoleName == "Owner"))
                .ProjectTo<OwnerNameModel>()
                .ToList();
        }

        public IEnumerable<string> GetRolesNamesForUser(string userName)
        {
            return this.db.Users.Where(u => u.UserName == userName).SelectMany(u => u.RolesNames.Select(r => r.RoleName));
        }
    }
}
