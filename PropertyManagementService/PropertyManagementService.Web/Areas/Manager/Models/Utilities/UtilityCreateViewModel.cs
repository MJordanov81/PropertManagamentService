namespace PropertyManagementService.Web.Areas.Manager.Models.Utilities
{
    using PropertyManagementService.Domain.Infrastructure.Enum;
    using System.ComponentModel.DataAnnotations;

    public class UtilityCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 symbols long.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description must be between 10 and 50 symbols long.", MinimumLength = 10)]
        public string Description { get; set; }

        public int BuildingId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Routine: ")]
        public Routine Routine { get; set; }

        [Display(Name = "Is calculated per resident?")]
        public bool IsPerResident { get; set; }

        [Display(Name = "Is unsubscribable?")]
        public bool IsSubscribable { get; set; }
    }
}
