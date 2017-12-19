namespace PropertyManagementService.Web.Areas.Manager.Models.Apartments
{
    using PropertyManagementService.Services.Models.User;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ApartmentCreateViewModel
    {
        [Required]
        [StringLength(10,ErrorMessage = "Number must be between 1 and 10 symbols long.", MinimumLength = 1)]
        public string Number { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Must greater or equal to zero.")]
        public int Residents { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Must greater or equal to zero.")]
        public int Dogs { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Must greater or equal to zero.")]
        public int Area { get; set; }

        [Required (ErrorMessage = "Owner is required.")]
        public string OwnerId { get; set; }

        public int BuildingId { get; set; }

        public IEnumerable<OwnerNameModel> Owners { get; set; } = new List<OwnerNameModel>();
    }
}
