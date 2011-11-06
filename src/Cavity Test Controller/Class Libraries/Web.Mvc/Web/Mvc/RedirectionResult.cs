namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public abstract class RedirectionResult : ActionResult
    {
        protected RedirectionResult()
            : this("/")
        {
        }

        protected RedirectionResult(string location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            if (0 == location.Length)
            {
                throw new ArgumentOutOfRangeException("location");
            }

            Location = location;
        }

        public string Location { get; protected set; }

        public abstract HttpStatusCode StatusCode { get; }

        public static string Content(DateTime date, 
                                     AbsoluteUri location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            return "<html>" +
                   "<head>" +
                   "<title>The information you requested has been found</title>" +
                   "<meta value=\"" + date.ToXmlString() + "\" />" +
                   "</head>" +
                   "<body>" +
                   "<h1>The information you requested has been found</h1>" +
                   "<p><a id=\"location\" href=\"" + location + "\">" + HttpUtility.HtmlEncode(location) + "</a></p>" +
                   "</body>" +
                   "</html>";
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            AbsoluteUri location = Location.Contains("://")
                                       ? new Uri(Location)
                                       : new Uri(context.HttpContext.Request.Url, new Uri(Location, UriKind.Relative));

            response.StatusCode = (int)StatusCode;
            response.AppendHeader("Location", location);

            SetCache(response.Cache);

            response.ContentType = "text/html";
            response.Write(Content(DateTime.UtcNow, location));
        }

        public abstract void SetCache(HttpCachePolicyBase cache);
    }
}