namespace PropertyManagementService.Services.Contracts
{
    using Models.Utility;
    using PropertyManagementService.Domain.Infrastructure.Enum;

    public interface IUtilityService
    {
        UtilitiesPaginatedModel GetUtilitiesForBuilding(int buildingId, string search);

        void Create(string currentUserId, string name, string description, decimal price, bool isSubscribable, Routine routine, bool isPerResident, int buildingId);
    }
}
