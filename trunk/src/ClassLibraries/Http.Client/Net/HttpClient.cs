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
            this.Http = http;
        }

        public string UserAgent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserAgent>().Value;
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
                return this._http;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._http = value;
            }
        }

        public void Navigate(IHttpRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            this.Response = this.Http.Send(request);
        }

        public override string ToString()
        {
            return this.UserAgent;
        }
    }
}