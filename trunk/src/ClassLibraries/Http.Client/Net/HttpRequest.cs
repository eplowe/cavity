namespace Cavity.Net
{
    using System;

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

        public RequestLine RequestLine
        {
            get
            {
                return this._requestLine;
            }

            set
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

        public IHttpResponse ToResponse(IHttpClient client)
        {
            if (null == client)
            {
                throw new ArgumentNullException("client");
            }

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.RequestLine;
        }
    }
}