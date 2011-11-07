namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public sealed class NotAcceptableResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}