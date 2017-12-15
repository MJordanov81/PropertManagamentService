namespace PropertyManagementService.Web.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using PropertyManagementService.Data;
    using System.IO;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PropertyManagementServiceDbContext>
    {
        public PropertyManagementServiceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<PropertyManagementServiceDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new PropertyManagementServiceDbContext(builder.Options);
        }
    }
}
