namespace PropertyManagementService.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Building
    {
        public int Id { get; set; }

        [Required]
        public string Contract { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime ServiceStartDate { get; set; }

        public DateTime? ServiceEndDate { get; set; }

        public string ManagerId { get; set; }

        public User Manager { get; set; }

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

        public ICollection<BuildingUtility> Utilities { get; set; } = new List<BuildingUtility>();
    }
}
