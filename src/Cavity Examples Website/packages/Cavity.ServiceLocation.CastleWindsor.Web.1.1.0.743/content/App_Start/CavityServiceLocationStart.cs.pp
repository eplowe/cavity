using Cavity.Configuration;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.CavityServiceLocationStart), "PreApplicationStart")]

namespace $rootnamespace$.App_Start
{
    public static class CavityServiceLocationStart
    {
        public static void PreApplicationStart()
        {
            Config.Section<ServiceLocation>("service.location").Provider.Configure();
        }
    }
}