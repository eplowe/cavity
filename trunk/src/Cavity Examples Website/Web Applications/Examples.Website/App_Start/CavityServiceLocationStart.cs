using Cavity.Configuration;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Cavity.App_Start.CavityServiceLocationStart), "PreApplicationStart")]

namespace Cavity.App_Start
{
    public static class CavityServiceLocationStart
    {
        public static void PreApplicationStart()
        {
            Config.Section<ServiceLocation>("service.location").Provider.Configure();
        }
    }
}