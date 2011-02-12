namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Net;
    using System.Net.Mime;
    using Cavity.Xml;

    public sealed class HttpExpectations : IRequestAcceptContent,
                                           IRequestAcceptLanguage,
                                           IRequestMethod,
                                           IResponseStatus,
                                           IResponseCacheControl,
                                           IResponseCacheConditionals,
                                           IResponseContentLanguage,
                                           IResponseContentMD5,
                                           IResponseContent,
                                           IResponseHtml,
                                           IResponseXml,
                                           ITestHttp
    {
        private IWebRequest _request;

        private HttpExpectations()
        {
            Expectations = new Collection<ITestHttpExpectation>();
        }

        private HttpExpectations(AbsoluteUri requestUri)
            : this()
        {
            Request = new HttpRequestDefinition(requestUri);
        }

        public IHttpContent Content { get; set; }

        public ICollection<ITestHttpExpectation> Expectations { get; private set; }

        public IWebRequest Request
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

        string ITestHttp.Location
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ITestHttp.Result
        {
            get
            {
                return false;
            }
        }

        private Response Response { get; set; }

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
            return (this as IRequestMethod).Use("DELETE");
        }

        IResponseStatus IRequestMethod.Get()
        {
            return (this as IRequestMethod).Use("GET");
        }

        IResponseStatus IRequestMethod.Head()
        {
            return (this as IRequestMethod).Use("HEAD");
        }

        IResponseStatus IRequestMethod.Options()
        {
            return (this as IRequestMethod).Use("OPTIONS");
        }

        IResponseStatus IRequestMethod.Post(IHttpContent content)
        {
            return (this as IRequestMethod).Use("POST", content);
        }

        IResponseStatus IRequestMethod.Put(IHttpContent content)
        {
            return (this as IRequestMethod).Use("PUT", content);
        }

        IResponseStatus IRequestMethod.Use(string method)
        {
            if (null == method)
            {
                throw new ArgumentNullException("method");
            }

            if (0 == method.Length)
            {
                throw new ArgumentOutOfRangeException("method");
            }

            Request.Method = method;
            if ("GET".Equals(method, StringComparison.OrdinalIgnoreCase))
            {
                Expectations.Add(new HttpHeadTest(Request));
            }

            return this;
        }

        IResponseStatus IRequestMethod.Use(string method,
                                           IHttpContent content)
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

        IResponseContentLanguage IResponseCacheConditionals.IgnoreCacheConditionals()
        {
            return this;
        }

        IResponseContentLanguage IResponseCacheConditionals.WithEtag()
        {
            Expectations.Add(new HttpResponseHeaderTest(HttpResponseHeader.ETag));
            return this;
        }

        IResponseContentLanguage IResponseCacheConditionals.WithExpires()
        {
            Expectations.Add(new HttpResponseHeaderTest(HttpResponseHeader.Expires));
            return this;
        }

        IResponseContentLanguage IResponseCacheConditionals.WithLastModified()
        {
            Expectations.Add(new HttpResponseHeaderTest(HttpResponseHeader.LastModified));
            return this;
        }

        IResponseCacheConditionals IResponseCacheControl.HasCacheControl(string value)
        {
            Expectations.Add(new HttpResponseCacheControlTest(value));
            return this;
        }

        IResponseContentLanguage IResponseCacheControl.HasCacheControlNone()
        {
            return (this as IResponseCacheControl).HasCacheControl("no-cache") as IResponseContentLanguage;
        }

        IResponseCacheConditionals IResponseCacheControl.HasCacheControlPrivate()
        {
            return (this as IResponseCacheControl).HasCacheControl("private");
        }

        IResponseCacheConditionals IResponseCacheControl.HasCacheControlPublic()
        {
            return (this as IResponseCacheControl).HasCacheControl("public");
        }

        IResponseContentLanguage IResponseCacheControl.IgnoreCacheControl()
        {
            return this;
        }

        ITestHttp IResponseContent.ResponseHasNoContent()
        {
            Expectations.Add(new HttpResponseContentTypeTest(null));

            // Response = NoContentResponse.Request(Request, Content);
            return this;
        }

        ITestHttp IResponseContent.ResponseIs(ContentType type)
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsApplicationJson()
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsApplicationJson(ContentType type)
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseContent.ResponseIsApplicationXhtml()
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseContent.ResponseIsApplicationXml()
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseContent.ResponseIsApplicationXml(ContentType type)
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsImageIcon()
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsTextCss()
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseContent.ResponseIsTextHtml()
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsTextJavaScript()
        {
            throw new NotImplementedException();
        }

        ITestHttp IResponseContent.ResponseIsTextPlain()
        {
            throw new NotImplementedException();
        }

        IResponseContentMD5 IResponseContentLanguage.HasContentLanguage(CultureInfo language)
        {
            return (this as IResponseContentLanguage).HasContentLanguage(null == language ? string.Empty : language.Name);
        }

        IResponseContentMD5 IResponseContentLanguage.HasContentLanguage(string language)
        {
            Expectations.Add(new HttpResponseContentLanguageTest(language));
            return this;
        }

        IResponseContentMD5 IResponseContentLanguage.IgnoreContentLanguage()
        {
            return this;
        }

        IResponseContent IResponseContentMD5.HasContentMD5()
        {
            Expectations.Add(new HttpResponseContentMD5Test());
            return this;
        }

        IResponseContent IResponseContentMD5.IgnoreContentMD5()
        {
            return this;
        }

        IResponseHtml IResponseHtml.Evaluate<T>(T expected,
                                                params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseHtml.EvaluateFalse(params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseHtml.EvaluateTrue(params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseHtml.HasRobotsTag(string value)
        {
            throw new NotImplementedException();
        }

        IResponseHtml IResponseHtml.HasStyleSheetLink(string href)
        {
            throw new NotImplementedException();
        }

        IResponseCacheControl IResponseStatus.Is(HttpStatusCode status)
        {
            Expectations.Add(new HttpStatusCodeTest(status));
            return this;
        }

        ITestHttp IResponseStatus.IsSeeOther(AbsoluteUri location)
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseXml.Evaluate<T>(T expected,
                                              params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseXml.Evaluate<T>(T expected,
                                              string xpath,
                                              params XmlNamespace[] namespaces)
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseXml.EvaluateFalse(params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        IResponseXml IResponseXml.EvaluateTrue(params string[] xpaths)
        {
            throw new NotImplementedException();
        }

        ITestHttp ITestHttp.HasContentLocation(AbsoluteUri location)
        {
            throw new NotImplementedException();
        }
    }
}