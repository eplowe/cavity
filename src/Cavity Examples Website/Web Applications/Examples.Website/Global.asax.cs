namespace Cavity
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Diagnostics;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            if (null == filters)
            {
                throw new ArgumentNullException("filters");
            }

            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Register(typeof(MvcApplication).Assembly.GetTypes());
            routes.Register<NotFoundController>();
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The runtime requires this to be an instance member.")]
        protected void Application_Start()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}