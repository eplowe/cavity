namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    public class TemporaryRedirectResult : RedirectionResult
    {
        public TemporaryRedirectResult(string location)
            : base(location)
        {
        }

        public DateTime? Expires { get; set; }

        public override HttpStatusCode StatusCode
        {
            get
            {
                return HttpStatusCode.TemporaryRedirect;
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