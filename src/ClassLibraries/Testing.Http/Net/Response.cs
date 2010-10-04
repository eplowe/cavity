namespace Cavity.Net
{
    using System;
    using System.Net;
    using System.Text;

    public sealed class Response
    {
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;
        private AbsoluteUri _location;

        public Response(HttpStatusCode status, AbsoluteUri location, WebHeaderCollection headers, CookieCollection cookies)
            : this()
        {
            Cookies = cookies;
            Headers = headers;
            Location = location;
            Status = status;
        }

        private Response()
        {
        }

        public CookieCollection Cookies
        {
            get
            {
                return _cookies;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _cookies = value;
            }
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return _headers;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _headers = value;
            }
        }

        public AbsoluteUri Location
        {
            get
            {
                return _location;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _location = value;
            }
        }

        public HttpStatusCode Status { get; private set; }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.AppendLine(Location);
            buffer.Append(Environment.NewLine);
            buffer.Append("HTTP/1.1 ");
            buffer.Append((int)Status);
            buffer.Append(" ");
            buffer.Append(Status.ToString("g"));
            buffer.Append(Environment.NewLine);
            for (var i = 0; i < Headers.Count; i++)
            {
                buffer.Append(Headers.Keys[i]);
                buffer.Append(": ");
                buffer.Append(Headers[i]);
                buffer.Append(Environment.NewLine);
            }

            return buffer.ToString();
        }
    }
}