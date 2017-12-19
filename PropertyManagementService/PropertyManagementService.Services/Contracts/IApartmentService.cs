namespace PropertyManagementService.Services.Contracts
{
    public interface IApartmentService
    {
        void Create(string number, int residents, int dogs, int area, string ownerId, int buildingId);
    }
}
