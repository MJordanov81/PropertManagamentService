namespace PropertyManagementService.Services.Contracts
{
    using PropertyManagementService.Services.Models.Bill;

    public interface IBillService
    {
        BillsForBuildingModel GetBillsForBuilding(int buildingId, bool isConfirmed = true);

        void GenerateBills(int buildingId, int period, int year);
    }
}
