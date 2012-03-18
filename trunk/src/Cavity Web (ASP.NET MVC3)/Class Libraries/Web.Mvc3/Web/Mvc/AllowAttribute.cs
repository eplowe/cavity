namespace Cavity.Web.Mvc
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Cavity.IO;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AllowAttribute : ActionFilterAttribute
    {
        public AllowAttribute(string methods)
            : this()
        {
            Methods = methods;
        }

        private AllowAttribute()
        {
        }

        public string Methods { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            base.OnActionExecuting(filterContext);

            var request = filterContext.HttpContext.Request;

            var options = "OPTIONS".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase);

            if (!options &&
                IsAllowed(request.HttpMethod))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;
            response.Clear();
            var filter = response.Filter as WrappedStream;
            if (null != filter)
            {
                response.Filter = filter.UnderlyingStream;
            }

            if (options)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }

            response.AppendHeader("Allow", Methods ?? "OPTIONS");
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(DateTime.UtcNow.AddDays(1));
            response.End();
        }

        private bool IsAllowed(string method)
        {
            if (string.IsNullOrEmpty(Methods))
            {
                return true;
            }

            return Methods
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Any(part => part.Trim().Equals(method, StringComparison.OrdinalIgnoreCase));
        }
    }
}