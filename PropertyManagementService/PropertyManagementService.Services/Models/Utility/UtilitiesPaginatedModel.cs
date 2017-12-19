namespace PropertyManagementService.Services.Models.Utility
{
    using System.Collections.Generic;

    public class UtilitiesPaginatedModel
    {
        public IList<UtilityBuildingListModel> Utilities { get; set; }

        public int BuildingId { get; set; }

        //pagination properties
        public int ItemsPerPage { get; set; }

        public int ItemsCount { get; set; }

        public int Page { get; set; }

        public string Search { get; set; }
    }
}
