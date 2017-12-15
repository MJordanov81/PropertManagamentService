namespace PropertyManagementService.Domain
{
    using Infrastructure.Enum;
    using System.ComponentModel.DataAnnotations;

    public class UtilityBuilding
    {
        public decimal Price { get; set; }

        public bool IsSubscribable { get; set; }

        public Routine Routine { get; set; }

        public bool IsPerResident { get; set; }

        public int UtilityId { get; set; }

        public Utility Utility { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }
    }
}
