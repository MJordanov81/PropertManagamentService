namespace PropertyManagementService.Domain
{
    public class UnsubscribedUtility
    {
        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public int BuildingUtilityId { get; set; }

        public BuildingUtility BuildingUtility { get; set; }
    }
}
