namespace PropertyManagementService.Services.Models.Building
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;

    public class BuildingShortDataModel : IMapFrom<Building>
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string Contract { get; set; }
    }
}
