namespace Cavity.Net
{
    using System.Net;

    public class HttpWebClient : DisposableObject,
                                 IHttpClient
    {
        public HttpWebClient()
        {
            Cookies = new CookieContainer();
        }

        public bool AutoRedirect { get; set; }

        public CookieContainer Cookies { get; private set; }

        protected override void OnDispose()
        {
        }
    }
}