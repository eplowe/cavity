namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Configuration;

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (null == filters)
            {
                throw new ArgumentNullException("filters");
            }

            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Root", 
                string.Empty, 
                new
                {
                    controller = "Root", 
                    action = "Redirect"
                });

            routes.MapRoute(
                "404",
                "*",
                new
                {
                    controller = "NotFound",
                    action = "HtmlRepresentation"
                });
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The runtime requires this to be an instance member.")]
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            Config.Section<ServiceLocation>("service.location").Provider.Configure();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}