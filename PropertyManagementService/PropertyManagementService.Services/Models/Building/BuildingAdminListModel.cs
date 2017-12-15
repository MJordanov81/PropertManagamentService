namespace PropertyManagementService.Services.Models.Building
{
    using PropertyManagementService.Domain;
    using PropertyManagementService.Common;
    using System;
    using AutoMapper;
    using System.Linq;

    public class BuildingAdminListModel : IMapFrom<Building>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Contract { get; set; }

        public string Address { get; set; }

        public DateTime ServiceStartDate { get; set; }

        public DateTime? ServiceEndDate { get; set; }

        public string ManagerEmail { get; set; }

        public int ApartmentsCount { get; set; }

        public int ResidentsCount { get; set; }

        public int DogsCount { get; set; }

        public int ApartmentsTotalArea { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Building, BuildingAdminListModel>()
                .ForMember(m => m.ManagerEmail, cfg => cfg.MapFrom(b => b.Manager.Email))
                .ForMember(m => m.ApartmentsCount, cfg => cfg.MapFrom(b => b.Apartments.Count))
                .ForMember(m => m.ApartmentsTotalArea, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Area)))
                .ForMember(m => m.ResidentsCount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Residents)))
                .ForMember(m => m.DogsCount, cfg => cfg.MapFrom(b => b.Apartments.Sum(a => a.Dogs)));
        }
    }
}
