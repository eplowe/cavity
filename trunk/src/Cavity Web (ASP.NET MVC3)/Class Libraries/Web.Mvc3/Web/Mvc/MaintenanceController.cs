namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Web.Routing;

    [InternalServerError]
    public sealed class MaintenanceController : Controller,
                                                IRegisterRoutes
    {
        public ActionResult HtmlRepresentation()
        {
            Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            return View();
        }

        void IRegisterRoutes.Register(RouteCollection routes)
        {
            if (null == routes)
            {
                throw new ArgumentNullException("routes");
            }

            routes.MapRoute(
                            "Maintenance",
                            "{*url}",
                            new
                                {
                                    controller = "Maintenance",
                                    action = "HtmlRepresentation"
                                });
        }
    }
}