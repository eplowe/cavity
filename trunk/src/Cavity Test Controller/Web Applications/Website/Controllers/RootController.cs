namespace Cavity.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using Cavity.Web.Mvc;

    public sealed class RootController : Controller
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The runtime requires this to be an instance member")]
        public ActionResult Redirect()
        {
            return new FoundResult("/today");
        }
    }
}