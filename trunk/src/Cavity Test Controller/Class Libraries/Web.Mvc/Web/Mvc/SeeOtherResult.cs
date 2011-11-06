namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    public class SeeOtherResult : RedirectionResult
    {
        public SeeOtherResult(string location)
            : base(location)
        {
        }

        public override HttpStatusCode StatusCode
        {
            get
            {
                return HttpStatusCode.SeeOther;
            }
        }

        public override void SetCache(HttpCachePolicyBase cache)
        {
            if (null == cache)
            {
                throw new ArgumentNullException("cache");
            }

            cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}