namespace Cavity.Net
{
    using System;
    using System.IO;

    public sealed class HttpRequest : ValueObject<HttpRequest>, IHttpRequest
    {
        private RequestLine _requestLine;

        public HttpRequest(RequestLine requestLine)
            : this()
        {
            this.RequestLine = requestLine;
        }

        private HttpRequest()
        {
            this.RegisterProperty(x => x.RequestLine);
        }

        public Uri AbsoluteUri
        {
            get
            {
                return new Uri(this.RequestLine.RequestUri, UriKind.RelativeOrAbsolute);
            }
        }

        public IHttpBody Body
        {
            get;
            private set;
        }

        public IHttpHeaderCollection Headers
        {
            get;
            private set;
        }

        public RequestLine RequestLine
        {
            get
            {
                return this._requestLine;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._requestLine = value;
            }
        }

        public static implicit operator HttpRequest(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpRequest : HttpRequest.Parse(value);
        }

        public static HttpRequest Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new HttpRequest(RequestLine.Parse(value));
        }

        public void Write(StreamWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.RequestLine;
        }
    }
}