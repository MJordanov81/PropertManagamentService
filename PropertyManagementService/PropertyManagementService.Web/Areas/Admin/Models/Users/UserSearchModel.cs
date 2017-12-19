namespace PropertyManagementService.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;

    public class UserSearchModel
    {
        public Dictionary<string, bool> IncludedRoles { get; set; } = new Dictionary<string, bool>();

        public string SearchString { get; set; }
    }
}
