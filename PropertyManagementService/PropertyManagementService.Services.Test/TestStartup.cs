namespace PropertyManagementService.Services.Test
{
    using AutoMapper;
    using PropertyManagementService.Web.Infrastructure.Mapping;

    public class TestStartup
    {
        private static bool isInitialized;

        public static void Initialize()
        {
            if (!isInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfile()));

                isInitialized = true;
            }
        }
    }
}
