namespace Cavity.Web.Mvc
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Globalization;
    using Cavity.Web.Routing;

    public sealed class DummyController : LanguageController, IRegisterRoutes
    {
        public override IEnumerable<Language> Languages
        {
            get
            {
                yield return "en";
            }
        }

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

        [ContentNegotiation(".xml", "application/xml")]
        public ActionResult Example()
        {
            return null;
        }
    }
}