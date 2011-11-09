namespace Cavity.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using Cavity.IO;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ContentMD5Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            base.OnActionExecuted(filterContext);

            var response = filterContext.HttpContext.Response;

            var filter = response.Filter as WrappedStream;
            if (null == filter)
            {
                return;
            }

            var md5 = filter.ContentMD5;
            if (!string.IsNullOrEmpty(md5))
            {
                response.AppendHeader("Content-MD5", md5);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            base.OnActionExecuting(filterContext);

            var response = filterContext.HttpContext.Response;

            response.BufferOutput = true;
            response.Filter = new WrappedStream(response.Filter);
        }
    }
}