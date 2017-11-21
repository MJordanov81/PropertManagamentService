namespace PropertyManagementService.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Domain;

    public class PropertyManagementServiceDbContext : IdentityDbContext<User>
    {
        public PropertyManagementServiceDbContext(DbContextOptions<PropertyManagementServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
        }
    }
}
