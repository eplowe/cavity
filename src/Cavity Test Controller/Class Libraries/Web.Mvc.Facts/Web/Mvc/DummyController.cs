namespace Cavity.Web.Mvc
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Web.Routing;

    public sealed class DummyController : Controller, IRegisterRoutes
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                "Dummy", 
                "example", 
                new
                {
                    controller = "Dummy", 
                    action = "Example"
                });
        }
    }
}