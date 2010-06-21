namespace Cavity.Net
{
    using System;
    using System.Text;

    public sealed class HttpResponse : ValueObject<HttpResponse>, IHttpResponse
    {
        private IHttpBody _body;
        private IHttpHeaderCollection _headers;
        private StatusLine _statusLine;

        public HttpResponse(StatusLine statusLine, IHttpHeaderCollection headers)
            : this()
        {
            this.StatusLine = statusLine;
            this.Headers = headers;
        }

        public HttpResponse(StatusLine statusLine, IHttpHeaderCollection headers, IHttpBody body)
            : this()
        {
            this.StatusLine = statusLine;
            this.Headers = headers;
            this.Body = body;
        }

        private HttpResponse()
        {
            this.RegisterProperty(x => x.StatusLine);
            this.RegisterProperty(x => x.Headers);
            this.RegisterProperty(x => x.Body);
        }

        public IHttpBody Body
        {
            get
            {
                return this._body;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._body = value;
            }
        }

        public IHttpHeaderCollection Headers
        {
            get
            {
                return this._headers;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._headers = value;
            }
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
            else if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            string[] lines = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder headers = new StringBuilder();
            StringBuilder body = null;
            for (int i = 1; i < lines.Length; i++)
            {
                if (null == body)
                {
                    if (0 == lines[i].Length)
                    {
                        body = new StringBuilder();
                    }
                    else
                    {
                        headers.AppendLine(lines[i]);
                    }
                }
                else
                {
                    body.AppendLine(lines[i]);
                }
            }

            return new HttpResponse(
                StatusLine.Parse(lines[0]),
                HttpHeaderCollection.Parse(headers.ToString()));
        }

        public override string ToString()
        {
            return this.StatusLine;
        }
    }
}