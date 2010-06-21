namespace Cavity.Net
{
    using System;

    public sealed class HttpResponse : ValueObject<HttpResponse>, IHttpResponse
    {
        private StatusLine _statusLine;

        public HttpResponse(StatusLine statusLine)
            : this()
        {
            this.StatusLine = statusLine;
        }

        private HttpResponse()
        {
            this.RegisterProperty(x => x.StatusLine);
        }

        public StatusLine StatusLine
        {
            get
            {
                return this._statusLine;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._statusLine = value;
            }
        }

        public static implicit operator HttpResponse(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpResponse : HttpResponse.Parse(value);
        }

        public static HttpResponse Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new HttpResponse(value);
        }

        public override string ToString()
        {
            return this.StatusLine;
        }
    }
}