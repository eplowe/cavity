namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    public sealed class DerivedRedirectionResult : RedirectionResult
    {
        private readonly HttpStatusCode _status;

        public DerivedRedirectionResult()
            : this(0)
        {
        }

        public DerivedRedirectionResult(HttpStatusCode status)
        {
            _status = status;
        }

        public DerivedRedirectionResult(string location)
            : base(location)
        {
        }

        public override HttpStatusCode StatusCode
        {
            get
            {
                return _status;
            }
        }

        public override void SetCache(HttpCachePolicyBase cache)
        {
            if (null != cache)
            {
                cache.SetCacheability(HttpCacheability.Server);
                cache.SetExpires(DateTime.UtcNow.AddDays(1));
            }
        }
    }
}