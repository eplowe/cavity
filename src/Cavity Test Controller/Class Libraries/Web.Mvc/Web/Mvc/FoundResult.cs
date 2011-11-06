namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    public class FoundResult : RedirectionResult
    {
        public FoundResult(string location)
            : base(location)
        {
        }

        public DateTime? Expires { get; set; }

        public override HttpStatusCode StatusCode
        {
            get
            {
                return HttpStatusCode.Found;
            }
        }

        public override void SetCache(HttpCachePolicyBase cache)
        {
            if (null == cache)
            {
                throw new ArgumentNullException("cache");
            }

            if (!Expires.HasValue)
            {
                cache.SetCacheability(HttpCacheability.NoCache);
                return;
            }

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(Expires.Value);
        }
    }
}