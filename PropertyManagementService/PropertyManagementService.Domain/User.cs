namespace PropertyManagementService.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Building> BuildingsAsManager { get; set; } = new List<Building>();

        public ICollection<Apartment> ApartmentsAsOwner { get; set; } = new List<Apartment>();

        public ICollection<UserRoleName> RolesNames { get; set; } = new List<UserRoleName>();
    }
}
