namespace Cavity.Net
{
    using System;
    using System.Text;
    using Cavity.Net.Mime;

    public sealed class HttpResponse : HttpMessage, IHttpResponse
    {
        private StatusLine _statusLine;

        public HttpResponse(StatusLine statusLine, IHttpHeaderCollection headers)
            : this()
        {
            this.StatusLine = statusLine;
            this.Headers = headers;
        }

        public HttpResponse(StatusLine statusLine, IHttpHeaderCollection headers, IContent body)
            : base(headers, body)
        {
            this.StatusLine = statusLine;
        }

        private HttpResponse()
            : base()
        {
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