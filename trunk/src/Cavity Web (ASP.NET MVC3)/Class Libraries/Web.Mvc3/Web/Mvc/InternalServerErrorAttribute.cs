namespace Cavity.Web.Mvc
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Cavity.Diagnostics;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class InternalServerErrorAttribute : FilterAttribute, 
                                                       IExceptionFilter
    {
        private Type _interceptor;

        public InternalServerErrorAttribute()
            : this(typeof(InternalServerErrorViewResult))
        {
        }

        public InternalServerErrorAttribute(Type interceptor)
        {
            Interceptor = interceptor;
        }

        public Type Interceptor
        {
            get
            {
                return _interceptor;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (null == value.GetInterface("IInterceptInternalServerError"))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _interceptor = value;
            }
        }

        public void OnException(ExceptionContext filterContext)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);

            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var code = new HttpException(null, filterContext.Exception).GetHttpCode();
            if ((int)HttpStatusCode.InternalServerError != code)
            {
                return;
            }

            Trace.TraceError("{0}", filterContext.Exception);

            var interceptor = (IInterceptInternalServerError)Activator.CreateInstance(Interceptor);
            filterContext.Result = interceptor.View(filterContext);
            filterContext.ExceptionHandled = true;

            var response = filterContext.HttpContext.Response;
            response.Clear();
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.TrySkipIisCustomErrors = true;
        }
    }
}