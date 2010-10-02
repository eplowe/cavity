namespace Cavity.Net
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;

    public sealed class HttpExpectations : IRequestAcceptContent, IRequestAcceptLanguage, IRequestMethod, IResponseStatus, IResponseCacheControl, IResponseCacheConditionals, IResponseContentLanguage, IResponseContentMD5, IResponseContent, IResponseHtml, ITestHttp
    {
        private HttpRequestDefinition _request;

        private HttpExpectations()
        {
            Items = new Collection<ITestHttpExpectation>();
        }

        private HttpExpectations(AbsoluteUri requestUri)
            : this()
        {
            Request = new HttpRequestDefinition(requestUri);
        }

        public IHttpContent Content { get; set; }

        public HttpRequestDefinition Request
        {
            get
            {
                return _request;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _request = value;
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

        public static IRequestAcceptContent RequestUri(AbsoluteUri location)
        {
            return new HttpExpectations(location);
        }

        IRequestAcceptLanguage IRequestAcceptContent.Accept(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            Request.Headers["Accept"] = value;
            return this;
        }

        IRequestAcceptLanguage IRequestAcceptContent.AcceptAnyContent()
        {
            return (this as IRequestAcceptContent).Accept("*/*");
        }

        IRequestMethod IRequestAcceptLanguage.AcceptAnyLanguage()
        {
            return this;
        }

        IRequestMethod IRequestAcceptLanguage.AcceptLanguage(CultureInfo language)
        {
            if (null == language)
            {
                throw new ArgumentNullException("language");
            }

            return (this as IRequestAcceptLanguage).AcceptLanguage(language.Name);
        }

        IRequestMethod IRequestAcceptLanguage.AcceptLanguage(string language)
        {
            if (null == language)
            {
                throw new ArgumentNullException("language");
            }

            Request.Headers["Accept-Language"] = language;
            return this;
        }

        IResponseStatus IRequestMethod.Delete()
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Get()
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Get(bool head)
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Head()
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Options()
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Post(IHttpContent content)
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Put(IHttpContent content)
        {
            throw new NotImplementedException();
        }

        IResponseStatus IRequestMethod.Use(string method)
        {
            if (null == method)
            {
                throw new ArgumentNullException("method");
            }
            else if (0 == method.Length)
            {
                throw new ArgumentOutOfRangeException("method");
            }

            Request.Method = method;
            if ("GET".Equals(method, StringComparison.OrdinalIgnoreCase))
            {
                Items.Add(new HttpHeadTest(Request));
            }

            return this;
        }

        IResponseStatus IRequestMethod.Use(string method, IHttpContent content)
        {
            if (null == content)
            {
                throw new ArgumentNullException("content");
            }
            
            if ("DELETE".Equals(method, StringComparison.OrdinalIgnoreCase) ||
                "GET".Equals(method, StringComparison.OrdinalIgnoreCase) || 
                "HEAD".Equals(method, StringComparison.OrdinalIgnoreCase) || 
                "OPTIONS".Equals(method, StringComparison.OrdinalIgnoreCase) || 
                "TRACE".Equals(method, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentOutOfRangeException("method");
            }

            (this as IRequestMethod).Use(method);
            Content = content;
            return this;
        }
    }
}