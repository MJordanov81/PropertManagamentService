namespace PropertyManagementService.Web
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = BuildWebHost(args);

            Setup(webHost);

            webHost.Run();
        }

        private static async void Setup(IWebHost webHost)
        {
            await webHost.SeedRoles(new List<string> { "Admin", "Owner", "Manager" });
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
