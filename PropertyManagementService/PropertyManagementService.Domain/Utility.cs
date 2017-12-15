namespace PropertyManagementService.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Utility
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<UtilityBuilding> Buildings { get; set; } = new List<UtilityBuilding>();
    }
}
