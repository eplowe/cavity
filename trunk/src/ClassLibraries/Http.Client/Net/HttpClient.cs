namespace Cavity.Net
{
    using System;
    using Microsoft.Practices.ServiceLocation;

    public sealed class HttpClient : ComparableObject, IHttpClient
    {
        private IHttp _http;

        public HttpClient()
            : this(new Http())
        {
        }

        public HttpClient(IHttp http)
        {
            Http = http;
        }

        public string UserAgent
        {
            get
            {
                var userAgent = ServiceLocator.Current.GetInstance<IUserAgent>();

                return null == userAgent ? new UserAgent().Value : userAgent.Value;
            }
        }

        public IHttpResponse Response
        {
            get;
            private set;
        }

        private IHttp Http
        {
            get
            {
                return _http;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _http = value;
            }
        }

        public void Navigate(IHttpRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            Response = Http.Send(request);
        }

        public override string ToString()
        {
            return UserAgent;
        }
    }
}