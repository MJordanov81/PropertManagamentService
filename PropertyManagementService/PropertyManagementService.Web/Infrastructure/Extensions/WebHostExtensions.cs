namespace PropertyManagementService.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using PropertyManagementService.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class WebHostExtensions
    {
        public async static Task<IWebHost> SeedRoles(this IWebHost webHost, IList<string> rolesNames)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                foreach (string roleName in rolesNames)
                {
                    if (!roleManager.RoleExistsAsync(roleName).Result)
                    {
                        await roleManager.CreateAsync(new Role { Name = roleName });
                    }
                }
            }

            return webHost;
        }
    }
}
