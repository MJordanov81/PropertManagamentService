namespace PropertyManagementService.Services.Models.Building
{
    using PropertyManagementService.Common;
    using PropertyManagementService.Domain;
    using PropertyManagementService.Services.Models.User;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BuildingModifyDataModel : IMapFrom<Building>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Contract number must be less than 600 symbols long.")]
        [Display(Name = "Contract")]
        public string Contract { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Building address must be less than 600 symbols long.")]
        [Display(Name = "Building address")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Building service start date")]
        public DateTime ServiceStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Building service end date (optional)")]
        public DateTime? ServiceEndDate { get; set; }

        [Display(Name = "Manager")]
        public string ManagerId { get; set; }

        public IEnumerable<UserEmailModel> Managers { get; set; } = new List<UserEmailModel>();
    }
}
