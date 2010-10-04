namespace Cavity.Net
{
    using System;
    using Cavity.Data;

    public sealed class HttpRequestDefinition : IWebRequest
    {
        private AbsoluteUri _requestUri;

        public HttpRequestDefinition(AbsoluteUri requestUri)
            : this()
        {
            RequestUri = requestUri;
        }

        private HttpRequestDefinition()
        {
            Headers = new DataCollection();
        }

        public DataCollection Headers { get; private set; }

        public string Method { get; set; }

        public AbsoluteUri RequestUri
        {
            get
            {
                return _requestUri;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _requestUri = value;
            }
        }
    }
}