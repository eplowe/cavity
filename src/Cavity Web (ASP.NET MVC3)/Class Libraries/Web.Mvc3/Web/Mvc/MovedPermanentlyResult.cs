namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    public class MovedPermanentlyResult : RedirectionResult
    {
        public MovedPermanentlyResult(string location)
            : base(location)
        {
            Expires = DateTime.UtcNow.AddDays(1);
        }

        public DateTime Expires { get; set; }

        public override HttpStatusCode StatusCode
        {
            get
            {
                return HttpStatusCode.MovedPermanently;
            }
        }

        public override void SetCache(HttpCachePolicyBase cache)
        {
            if (null == cache)
            {
                throw new ArgumentNullException("cache");
            }

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(Expires);
        }
    }
}