[assembly: WebActivator.PreApplicationStartMethod(typeof(Cavity.App_Start.CavityDiagnosticsStart), "PreApplicationStart")]

namespace Cavity.App_Start
{
    public static class CavityDiagnosticsStart
    {
        public static void PreApplicationStart()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}