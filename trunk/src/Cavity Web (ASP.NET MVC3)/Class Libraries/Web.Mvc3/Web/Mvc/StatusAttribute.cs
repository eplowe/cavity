namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class StatusAttribute : ActionFilterAttribute
    {
        public StatusAttribute(HttpStatusCode code)
        {
            Code = code;
        }

        public HttpStatusCode Code { get; private set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            base.OnActionExecuted(filterContext);

            filterContext.HttpContext.Response.StatusCode = (int)Code;
        }
    }
}