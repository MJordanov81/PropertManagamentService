namespace PropertyManagementService.Web.Areas.Admin.Models.Buildings
{
    using PropertyManagementService.Services.Models.User;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BuildingCreateViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Contract number must be less than 600 symbols long.")]
        [Display(Name = "Contract")]
        public string Contract { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Building address must be less than 600 symbols long.")]
        [Display(Name = "Building address")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Building serviced as of")]
        public DateTime ServiceStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Building service end date (optional)")]
        public DateTime? ServiceEndDate { get; set; }

        [Display(Name = "Manager")]
        public string ManagerId { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public IEnumerable<UserEmailModel> Managers { get; set; } = new List<UserEmailModel>();
    }
}
