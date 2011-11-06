namespace Cavity.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    public sealed class NotFoundController : Controller
    {
        public ActionResult HtmlRepresentation()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            return View();
        }
    }
}