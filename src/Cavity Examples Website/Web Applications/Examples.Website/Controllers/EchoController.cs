namespace Cavity.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Cavity.Globalization;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;

    [Allow("GET, HEAD, OPTIONS, POST")]
    public sealed class EchoController : LanguageController, 
                                         IRegisterRoutes
    {
        public override IEnumerable<Language> Languages
        {
            get
            {
                yield return "en";
                yield return "fr";
            }
        }

        [ContentNegotiation(".html", "*/*, text/*, text/html")]
        public ActionResult HtmlRepresentation(CultureInfo language, 
                                               string key)
        {
            language.SetCurrentCulture();

            ViewBag.Key = key;

            return View(HttpContext.Cache[key]);
        }

        public ActionResult Post()
        {
            var key = AlphaDecimal.Random();

            HttpContext.Cache.Add(key, 
                                  Request.Form, 
                                  null, 
                                  DateTime.UtcNow.AddMinutes(30), 
                                  Cache.NoSlidingExpiration, 
                                  CacheItemPriority.Default, 
                                  null);

            return new SeeOtherResult("/echo/{0}".FormatWith(key));
        }

        void IRegisterRoutes.Register(RouteCollection routes)
        {
            if (null == routes)
            {
                throw new ArgumentNullException("routes");
            }

            routes.MapRoute(
                "Echo (POST)", 
                "echo", 
                new
                    {
                        controller = "Echo", 
                        action = "Post"
                    }, 
                new
                    {
                        method = new HttpMethodConstraint("POST")
                    });

            routes.MapRoute(
                "Echo (HTML)", 
                "echo/{key}.{language}.html", 
                new
                    {
                        controller = "Echo", 
                        action = "HtmlRepresentation", 
                        language = "fr"
                    });

            routes.MapRoute(
                "Echo (conneg)", 
                "echo/{key}.{language}", 
                new
                    {
                        controller = "Echo", 
                        action = "ContentNegotiation", 
                        language = "fr"
                    });

            routes.MapRoute(
                "Echo (lanneg)", 
                "echo/{key}", 
                new
                    {
                        controller = "Echo", 
                        action = "LanguageNegotiation"
                    });
        }
    }
}