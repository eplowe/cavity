namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Web.Routing;

    [Allow("DELETE, GET, HEAD, OPTIONS, POST, PUT")]
    public sealed class NotFoundController : Controller, IRegisterRoutes
    {
        public ActionResult HtmlRepresentation()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            ViewBag.Message = "The requested resource was not found.";

            return View();
        }

        void IRegisterRoutes.Register(RouteCollection routes)
        {
            if (null == routes)
            {
                throw new ArgumentNullException("routes");
            }

            routes.MapRoute(
                "404", 
                "{*url}", 
                new
                {
                    controller = "NotFound", 
                    action = "HtmlRepresentation"
                });
        }
    }
}