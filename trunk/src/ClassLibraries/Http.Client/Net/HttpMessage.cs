namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;

    public abstract class HttpMessage : ComparableObject, IHttpMessage
    {
        private IContent _body;

        private HttpHeaderCollection _headers;

        protected HttpMessage()
        {
            Headers = new HttpHeaderCollection();
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public IContent Body
        {
            get
            {
                return _body;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _body = value;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public HttpHeaderCollection Headers
        {
            get
            {
                return _headers;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _headers = value;
            }
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            using (var writer = new StringWriter(buffer, CultureInfo.InvariantCulture))
            {
                Write(writer);
                writer.Flush();
            }

            return buffer.ToString();
        }

        public virtual void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            var headers = new HttpHeaderCollection();
            headers.Read(reader);
            Headers = headers;

            var contentType = headers.ContentType;
            if (null != contentType)
            {
                Body = ToContent(
                    reader,
                    ServiceLocator.Current.GetInstance<IMediaType>(contentType.MediaType));
            }
        }

        public virtual void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            Headers.Write(writer);
            writer.WriteLine(string.Empty);
            if (null != Body)
            {
                Body.Write(writer);
            }
        }

        private static IContent ToContent(TextReader reader,
                                          IMediaType mediaType)
        {
            if (null == mediaType)
            {
                throw new ArgumentNullException("mediaType");
            }

            return mediaType.ToContent(reader);
        }
    }
}