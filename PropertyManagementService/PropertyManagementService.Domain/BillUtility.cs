namespace PropertyManagementService.Domain
{
    public class BillUtility
    {
        public int BillId { get; set; }

        public Bill Bill { get; set; }

        public int BuildingUtilityId { get; set; }

        public BuildingUtility BuildingUtility { get; set; }
    }
}
