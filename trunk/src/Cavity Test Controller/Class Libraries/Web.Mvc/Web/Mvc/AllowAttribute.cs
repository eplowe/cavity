namespace Cavity.Web.Mvc
{
    using System;
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
            var response = filterContext.HttpContext.Response;

            var options = "OPTIONS".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase);

            if (!options &&
                !IsNotAllowed(request.HttpMethod))
            {
                return;
            }

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

        private bool IsNotAllowed(string method)
        {
            var allowed = false;

            if (!string.IsNullOrEmpty(Methods))
            {
                var parts = Methods.Split(new[]
                {
                    ','
                }, 
                                          StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    if (part.Trim().Equals(method, StringComparison.OrdinalIgnoreCase))
                    {
                        allowed = true;
                    }
                }
            }

            return !allowed;
        }
    }
}