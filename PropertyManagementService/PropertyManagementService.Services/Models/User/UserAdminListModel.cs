namespace PropertyManagementService.Services.Models.User
{
    using AutoMapper;
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class UserAdminListModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int BuildingsAsManager { get; set; }

        public int ApartmentsAsOwner { get; set; }

        public ICollection<string> Roles { get; set; } = new List<string>();

        public void Configure(Profile profile)
        {
            profile.CreateMap<User, UserAdminListModel>()
                .ForMember(m => m.Email, cfg => cfg.MapFrom(u => u.UserName))
                .ForMember(m => m.BuildingsAsManager, cfg => cfg.MapFrom(u => u.BuildingsAsManager.Count))
                .ForMember(m => m.ApartmentsAsOwner, cfg => cfg.MapFrom(u => u.ApartmentsAsOwner.Count))
                .ForMember(m => m.Roles, cfg => cfg.MapFrom(u => u.RolesNames.Select(ur => ur.RoleName).ToList()));
        }
    }
}
