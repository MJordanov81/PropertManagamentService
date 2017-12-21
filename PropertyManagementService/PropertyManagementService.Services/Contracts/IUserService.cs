namespace PropertyManagementService.Services.Contracts
{
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Models.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        IEnumerable<UserEmailModel> GetUsersEmailsList(string role = null);

        UsersAdminPaginatedModel GetUsers(string search, IList<string> rolesSearch);

        Task AddRoleNameToUser(string userId, string roleName);

        UserModifyDataModel GetUserToEdit(string userId);

        IEnumerable<string> GetRolesNames();

        void EditUser(string userId, ICollection<string> userNewRoles);

        User GetUser(string userId);

        IEnumerable<OwnerNameModel> GetOwners();

        IEnumerable<string> GetRolesNamesForUser(string userName);
    }
}
