namespace PropertyManagementService.Services.Models.User
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class UserModifyDataModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; } = new List<string>();

        public void Configure(Profile profile)
        {
            profile.CreateMap<User, UserModifyDataModel>()
                .ForMember(m => m.Roles, cfg => cfg.MapFrom(u => u.RolesNames.Select(r => r.RoleName)));
        }
    }
}
