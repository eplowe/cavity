namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public sealed class HttpRequest : HttpMessage, IHttpRequest
    {
        private RequestLine _requestLine;

        public Uri AbsoluteUri
        {
            get
            {
                var value = new Uri(this.RequestLine.RequestUri, UriKind.RelativeOrAbsolute);

                if (!value.IsAbsoluteUri)
                {
                    value = new Uri(new Uri("http://" + this.Headers["Host"], UriKind.Absolute), value);
                }

                return value;
            }
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
            return object.ReferenceEquals(null, value) ? null as HttpRequest : HttpRequest.FromString(value);
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static HttpRequest FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var result = new HttpRequest();

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

        public override void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            this.RequestLine = reader.ReadLine();
            base.Read(reader);
        }

        public override void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WriteLine(this.RequestLine);
            base.Write(writer);
        }
    }
}