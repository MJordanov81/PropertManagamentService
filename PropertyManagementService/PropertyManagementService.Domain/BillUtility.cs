namespace PropertyManagementService.Domain
{
    public class BillUtility
    {
        public int BillId { get; set; }

        public Bill Bill { get; set; }

        public int UtilityBuildingId { get; set; }

        public UtilityBuilding UtilityBuilding { get; set; }
    }
}
