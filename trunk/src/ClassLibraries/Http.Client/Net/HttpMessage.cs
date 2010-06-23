namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;

    public abstract class HttpMessage : ComparableObject, IHttpMessage
    {
        private IContent _body;
        private HttpHeaderCollection _headers;

        protected HttpMessage(HttpHeaderCollection headers, IContent body)
            : this()
        {
            this.Headers = headers;
            this.Body = body;
        }

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

        public virtual void Read(StreamReader reader)
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
                this.Body = ServiceLocator.Current.GetInstance<IMediaType>(contentType.MediaType).ToBody(reader);
            }
        }

        public override string ToString()
        {
            string result = this.Headers;

            if (null != this.Body)
            {
                result += string.Concat(Environment.NewLine + this.Body.ToString());
            }

            return result;
        }
    }
}