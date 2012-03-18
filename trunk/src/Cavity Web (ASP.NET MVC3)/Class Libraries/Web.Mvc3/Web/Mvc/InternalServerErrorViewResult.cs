namespace Cavity.Web.Mvc
{
    using System;
    using System.Web.Mvc;

    public sealed class InternalServerErrorViewResult : ViewResult, 
                                                        IInterceptInternalServerError
    {
        public InternalServerErrorViewResult()
            : this("Error")
        {
        }

        public InternalServerErrorViewResult(string viewName)
        {
            if (null == viewName)
            {
                throw new ArgumentNullException("viewName");
            }

            if (0 == viewName.Length)
            {
                throw new ArgumentOutOfRangeException("viewName");
            }

            ViewName = viewName;
        }

        ActionResult IInterceptInternalServerError.View(ExceptionContext filterContext)
        {
            if (null == filterContext)
            {
                throw new ArgumentNullException("filterContext");
            }

            return this;
        }
    }
}