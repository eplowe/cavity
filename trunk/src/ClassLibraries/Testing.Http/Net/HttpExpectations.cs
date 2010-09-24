namespace Cavity.Net
{
    using System;
    using System.Collections.ObjectModel;

    public sealed class HttpExpectations : ITestHttp
    {
        private AbsoluteUri _requestUri;

        public HttpExpectations(AbsoluteUri requestUri)
            : this()
        {
            RequestUri = requestUri;
        }

        private HttpExpectations()
        {
            Items = new Collection<ITestHttpExpectation>();
        }

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

        bool ITestHttp.Result
        {
            get
            {
                return false;
            }
        }

        private Collection<ITestHttpExpectation> Items { get; set; }
    }
}