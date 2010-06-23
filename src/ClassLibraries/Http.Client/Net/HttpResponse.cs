namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Net.Mime;

    public sealed class HttpResponse : HttpMessage, IHttpResponse
    {
        private StatusLine _statusLine;

        public HttpResponse()
            : base()
        {
        }

        public HttpResponse(StatusLine statusLine, HttpHeaderCollection headers)
            : this()
        {
            this.StatusLine = statusLine;
            this.Headers = headers;
        }

        public HttpResponse(StatusLine statusLine, HttpHeaderCollection headers, IContent body)
            : base(headers, body)
        {
            this.StatusLine = statusLine;
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

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
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

            var result = new HttpResponse();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(value);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        result.Read(reader);
                    }
                }
            }

            return result;
        }

        public override void Read(StreamReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            this.StatusLine = reader.ReadLine();
            base.Read(reader);
        }

        public override string ToString()
        {
            return string.Concat(this.StatusLine, Environment.NewLine, base.ToString());
        }
    }
}