namespace PropertyManagementService.Services.Contracts
{
    using PropertyManagementService.Services.Models.Bill;
    using System.Threading.Tasks;

    public interface IBillService
    {
        BillsForBuildingModel GetBillsForBuilding(string search, int buildingId, bool isConfirmed = true);

        void GenerateBills(string currentUserId, int buildingId, int period, int year);

        Task<int> DeleteMultiple(string currentUserId, int[] billsIds);

        Task<int> ConfirmMultiple(string currentUserId, int[] billsIds);
    }
}
