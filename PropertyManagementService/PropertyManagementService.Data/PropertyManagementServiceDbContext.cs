namespace PropertyManagementService.Data
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PropertyManagementService.Domain;

    public class PropertyManagementServiceDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingUtility> BuildingUtilities { get; set; }

        public DbSet<UserRoleName> UserRoleNames { get; set; }

        public DbSet<BillUtility> BillUtilities { get; set; }

        public DbSet<UnsubscribedUtility> UnsubscribedUtilities { get; set; }

        public PropertyManagementServiceDbContext(DbContextOptions<PropertyManagementServiceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BuildingUtility>()
                .HasAlternateKey(u => u.Name);

            builder.Entity<Apartment>()
                .HasAlternateKey(a => a.Number);

            builder.Entity<UserRoleName>()
                .HasKey(r => new { r.UserId, r.RoleName });

            builder.Entity<UnsubscribedUtility>()
                .HasKey(uu => new { uu.ApartmentId, uu.BuildingUtilityId });

            builder.Entity<BillUtility>()
                .HasKey(bu => new { bu.BillId, bu.BuildingUtilityId });

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

            builder.Entity<Building>()
                .HasMany(b => b.Utilities)
                .WithOne(u => u.Building)
                .HasForeignKey(u => u.BuildingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Apartment>()
                .HasMany(a => a.UnsubscribedUtilities)
                .WithOne(uu => uu.Apartment)
                .HasForeignKey(uu => uu.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bill>()
                .HasOne(b => b.Apartment)
                .WithMany(a => a.Bills)
                .HasForeignKey(b => b.ApartmentId);

            builder.Entity<Bill>()
                .HasMany(b => b.Utilities)
                .WithOne(u => u.Bill)
                .HasForeignKey(u => u.BillId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.RolesNames)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            base.OnModelCreating(builder);
        }

        public bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
