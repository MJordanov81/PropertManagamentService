namespace PropertyManagementService.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Domain;

    public class PropertyManagementServiceDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Utility> Utilities { get; set; }

        public DbSet<UtilityBuilding> BuildingUtilities { get; set; }

        public PropertyManagementServiceDbContext(DbContextOptions<PropertyManagementServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UtilityBuilding>()
                .HasKey(ub => new { ub.UtilityId, ub.BuildingId });

            builder.Entity<UnsubscribedUtility>()
                .HasKey(uu => new { uu.ApartmentId, uu.UtilityId });

            builder.Entity<BillUtility>()
                .HasKey(bu => new { bu.BillId, bu.UtilityBuildingId });

            builder.Entity<User>()
                .HasMany(u => u.BuildingsAsManager)
                .WithOne(b => b.Manager)
                .HasForeignKey(b => b.ManagerId);

            builder.Entity<User>()
                .HasMany(u => u.ApartmentsAsOwner)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId);

            builder.Entity<Building>()
                .HasMany(b => b.Apartments)
                .WithOne(a => a.Building)
                .HasForeignKey(a => a.BuildingId);

            builder.Entity<Utility>()
                .HasMany(u => u.Buildings)
                .WithOne(b => b.Utility)
                .HasForeignKey(b => b.UtilityId);

            builder.Entity<Building>()
                .HasMany(b => b.Utilities)
                .WithOne(u => u.Building)
                .HasForeignKey(u => u.BuildingId);

            builder.Entity<Apartment>()
                .HasMany(a => a.UnsubscribedUtilities)
                .WithOne(uu => uu.Apartment)
                .HasForeignKey(uu => uu.ApartmentId);

            builder.Entity<Bill>()
                .HasOne(b => b.Apartment)
                .WithMany(a => a.Bills)
                .HasForeignKey(b => b.ApartmentId);

            builder.Entity<Bill>()
                .HasMany(b => b.Utilities)
                .WithOne(u => u.Bill)
                .HasForeignKey(u => u.BillId);

            base.OnModelCreating(builder);
        }
    }
}
