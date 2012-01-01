[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.CavityDiagnosticsStart), "PreApplicationStart")]

namespace $rootnamespace$.App_Start
{
    public static class CavityDiagnosticsStart
    {
        public static void PreApplicationStart()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}