namespace PropertyManagementService.Services.Models.Utility
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Domain.Infrastructure.Enum;

    public class UtilityBuildingListModel : IMapFrom<BuildingUtility>, IHaveCustomMapping
    {
        public int UtilityId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsSubscribable { get; set; }

        public Routine Routine { get; set; }

        public bool IsPerResident { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<BuildingUtility, UtilityBuildingListModel>()
                 .ForMember(m => m.Name, cfg => cfg.MapFrom(u => u.Name));
        }
    }
}


