namespace PropertyManagementService.Services.Models.User
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;

    public class OwnerNameModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
