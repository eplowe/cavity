namespace Cavity.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity.Diagnostics;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;

    [Allow("GET, HEAD, OPTIONS")]
    public sealed class RootController : Controller, IRegisterRoutes
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The runtime requires this to be an instance member")]
        public ActionResult Redirect()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            return new FoundResult("/{0}".FormatWith(DateTime.UtcNow.ToXmlString().Substring(0, 10)));
        }

        void IRegisterRoutes.Register(RouteCollection routes)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            if (null == routes)
            {
                throw new ArgumentNullException("routes");
            }

            routes.MapRoute(
                "Root",
                string.Empty,
                new
                {
                    controller = "Root",
                    action = "Redirect"
                });
        }
    }
}
