namespace PropertyManagementService.Services.Contracts
{
    using PropertyManagementService.Services.Models.User;
    using System.Collections.Generic;

    public interface IUserService
    {
        IEnumerable<UserEmailModel> GetUsersEmailsList(string role = null);
    }
}
