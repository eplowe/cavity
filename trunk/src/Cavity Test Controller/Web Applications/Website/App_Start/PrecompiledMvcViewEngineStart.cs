[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "namespace", Target = "Cavity.App_Start")]
[assembly: WebActivator.PreApplicationStartMethod(typeof(Cavity.App_Start.PrecompiledMvcViewEngineStart), "Start")]

namespace Cavity.App_Start
{
    using System.Web.Mvc;
    using System.Web.WebPages;
    using RazorGenerator.Mvc;

    public static class PrecompiledMvcViewEngineStart
    {
        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof(PrecompiledMvcViewEngineStart).Assembly);

            ViewEngines.Engines.Insert(0, engine);

            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}