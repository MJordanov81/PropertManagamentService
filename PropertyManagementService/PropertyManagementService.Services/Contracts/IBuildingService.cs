namespace PropertyManagementService.Services.Contracts
{
    using PropertyManagementService.Services.Models.Building;
    using System;

    public interface IBuildingService
    {
        void Create(string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId, string imageUrl);

        BuildingsPaginatedModel<TModel> GetBuildings<TModel>(string search, string managerId = null);

        BuildingModifyDataModel GetBuildingToEdit(int buildingId);

        void EditBuilding(int buildingId, string contract, string address, DateTime serviceStartDate, DateTime? serviceEndDate, string managerId, string imageUrl);

        BuildingManagerDetailsModel GetBuildingDetails(int buildingId);
    }
}
