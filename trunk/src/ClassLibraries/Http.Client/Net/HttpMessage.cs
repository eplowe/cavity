namespace Cavity.Net
{
    using System;
    using Cavity.Net.Mime;

    public abstract class HttpMessage : ComparableObject, IHttpMessage
    {
        private IContent _body;
        private IHttpHeaderCollection _headers;

        protected HttpMessage(IHttpHeaderCollection headers, IContent body)
            : this()
        {
            this.Headers = headers;
            this.Body = body;
        }

        protected HttpMessage()
        {
        }

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

        public IHttpHeaderCollection Headers
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
    }
}