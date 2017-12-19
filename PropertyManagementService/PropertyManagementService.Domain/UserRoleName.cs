namespace PropertyManagementService.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class UserRoleName
    {
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
