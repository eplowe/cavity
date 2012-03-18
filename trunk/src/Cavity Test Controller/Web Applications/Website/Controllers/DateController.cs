namespace Cavity.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Cavity.Globalization;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;

    [Allow("GET, HEAD, OPTIONS")]
    public sealed class DateController : LanguageController, 
                                         IRegisterRoutes
    {
        public override IEnumerable<Language> Languages
        {
            get
            {
                yield return "en";
            }
        }

        [ContentNegotiation(".html", "*/*, text/*, text/html")]
        public ActionResult HtmlRepresentation(CultureInfo language, 
                                               DateTime date)
        {
            language.SetCurrentCulture();

            return View(date);
        }

        void IRegisterRoutes.Register(RouteCollection routes)
        {
            if (null == routes)
            {
                throw new ArgumentNullException("routes");
            }

            routes.MapRoute(
                "Today", 
                "today.{language}.html", 
                new
                    {
                        controller = "Date", 
                        action = "HtmlRepresentation", 
                        language = "fr", 
                        date = DateTime.Today
                    });

            routes.MapRoute(
                "Today (conneg)", 
                "today.{language}", 
                new
                    {
                        controller = "Date", 
                        action = "ContentNegotiation", 
                        language = "fr"
                    });

            routes.MapRoute(
                "Today (lanneg)", 
                "today", 
                new
                    {
                        controller = "Date", 
                        action = "LanguageNegotiation"
                    });
        }
    }
}