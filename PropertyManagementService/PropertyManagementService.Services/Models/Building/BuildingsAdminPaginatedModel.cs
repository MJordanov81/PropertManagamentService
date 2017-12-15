namespace PropertyManagementService.Services.Models.Building
{
    using System.Collections.Generic;
    using System.Linq;

    public class BuildingsAdminPaginatedModel
    {
        public IList<BuildingAdminListModel> Buildings { get; set; }

        //pagination properties
        public int ItemsPerPage { get; set; }

        public int ItemsCount { get; set; }

        public int Page { get; set; }

        public string Search { get; set; }
    }
}
