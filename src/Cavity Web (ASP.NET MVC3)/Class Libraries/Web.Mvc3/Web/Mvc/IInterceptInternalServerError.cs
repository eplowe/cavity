namespace Cavity.Web.Mvc
{
    using System.Web.Mvc;

    public interface IInterceptInternalServerError
    {
        ActionResult View(ExceptionContext filterContext);
    }
}