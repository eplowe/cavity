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
            this.Headers = new HttpHeaderCollection();
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public IContent Body
        {
            get
            {
                return this._body;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._body = value;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public HttpHeaderCollection Headers
        {
            get
            {
                return this._headers;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._headers = value;
            }
        }

        public virtual void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            var headers = new HttpHeaderCollection();
            headers.Read(reader);
            this.Headers = headers;

            var contentType = headers.ContentType;
            if (null != contentType)
            {
                this.Body = ServiceLocator.Current.GetInstance<IMediaType>(contentType.MediaType).ToContent(reader);
            }
        }

        public virtual void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            this.Headers.Write(writer);
            if (null != this.Body)
            {
                writer.WriteLine(string.Empty);
                this.Body.Write(writer);
            }
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            using (var writer = new StringWriter(buffer, CultureInfo.InvariantCulture))
            {
                this.Write(writer);
                writer.Flush();
            }

            return buffer.ToString();
        }
    }
}