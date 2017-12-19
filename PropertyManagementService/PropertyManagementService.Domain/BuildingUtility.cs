namespace PropertyManagementService.Domain
{
    using Infrastructure.Enum;
    using System.ComponentModel.DataAnnotations;

    public class BuildingUtility
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 symbols long.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description must be between 10 and 50 symbols long.", MinimumLength = 10)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsSubscribable { get; set; }

        public Routine Routine { get; set; }

        public bool IsPerResident { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }
    }
}
