namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public sealed class HttpResponse : HttpMessage, IHttpResponse
    {
        private StatusLine _statusLine;

        public StatusLine StatusLine
        {
            get
            {
                return _statusLine;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _statusLine = value;
            }
        }

        public static implicit operator HttpResponse(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static HttpResponse FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var result = new HttpResponse();

            using (var stream = new MemoryStream())
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

            return result;
        }

        public override void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            StatusLine = reader.ReadLine();
            base.Read(reader);
        }

        public override void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WriteLine(StatusLine);
            base.Write(writer);
        }
    }
}