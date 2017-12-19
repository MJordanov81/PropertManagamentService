namespace PropertyManagementService.Services.Models.User
{
    using System.Collections.Generic;

    public class UsersAdminPaginatedModel
    {
        public IList<UserAdminListModel> Users { get; set; }

        //pagination properties
        public int ItemsPerPage { get; set; }

        public int ItemsCount { get; set; }

        public int Page { get; set; }

        public string Search { get; set; }

        public Dictionary<string, bool> IncludedRoles { get; set; } = new Dictionary<string, bool>();
    }
}
