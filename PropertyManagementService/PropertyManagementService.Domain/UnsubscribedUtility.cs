namespace PropertyManagementService.Domain
{
    public class UnsubscribedUtility
    {
        public int ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public int UtilityId { get; set; }

        public Utility Utility { get; set; }
    }
}
