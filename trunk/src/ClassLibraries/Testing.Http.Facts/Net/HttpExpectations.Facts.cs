namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using Xunit;

    public sealed class HttpExpectationsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpExpectations>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IRequestAcceptContent>()
                            .Implements<IRequestAcceptLanguage>()
                            .Implements<IRequestMethod>()
                            .Implements<IResponseStatus>()
                            .Implements<IResponseCacheControl>()
                            .Implements<IResponseCacheConditionals>()
                            .Implements<IResponseContentLanguage>()
                            .Implements<IResponseContentMD5>()
                            .Implements<IResponseContent>()
                            .Implements<IResponseHtml>()
                            .Implements<ITestHttp>()
                            .Result);
        }

        [Fact]
        public void IRequestAcceptContent_op_Accept_string()
        {
            const string value = "text/*";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .Accept(value);

            Assert.True(obj.Request.Headers.Contains("Accept", value));
        }

        [Fact]
        public void IRequestAcceptContent_op_Accept_stringEmpty()
        {
            var value = string.Empty;
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .Accept(value);

            Assert.True(obj.Request.Headers.Contains("Accept", value));
        }

        [Fact]
        public void IRequestAcceptContent_op_Accept_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .Accept(null));
        }

        [Fact]
        public void IRequestAcceptContent_op_AcceptAnyContent()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent();

            Assert.False(obj.Request.Headers.Contains("Accept"));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptAnyLanguage()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage();

            Assert.False(obj.Request.Headers.Contains("Accept-Language"));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_CultureInfo()
        {
            const string value = "en-gb";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptLanguage(new CultureInfo(value));

            Assert.True(obj.Request.Headers.Contains("Accept-Language", value));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_CultureInfoNull()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptLanguage(null as CultureInfo);

            Assert.False(obj.Request.Headers.Contains("Accept-Language"));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_string()
        {
            const string value = "en-gb, en-US;q=0.8, en;q=0.7";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptLanguage(value);

            Assert.True(obj.Request.Headers.Contains("Accept-Language", value));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_stringEmpty()
        {
            var value = string.Empty;
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptLanguage(value);

            Assert.True(obj.Request.Headers.Contains("Accept-Language", value));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptLanguage(null as string));
        }

        [Fact]
        public void IRequestMethod_op_Use_string_IHttpContent()
        {
            const string method = "POST";
            var content = new IHttpContentDummy();
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use(method, content);

            Assert.Equal(method, obj.Request.Method);
            Assert.Same(content, obj.Content);
        }

        [Fact]
        public void IRequestMethod_op_UseDelete_stringTrace_IHttpContent()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("DELETE", new IHttpContentDummy()));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringGet_IHttpContent()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("GET", new IHttpContentDummy()));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringHead_IHttpContent()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("HEAD", new IHttpContentDummy()));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringOptions_IHttpContent()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("OPTIONS", new IHttpContentDummy()));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringTrace_IHttpContent()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("TRACE", new IHttpContentDummy()));
        }

        [Fact]
        public void IRequestMethod_op_Use_string_IHttpContentNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                .AcceptAnyContent()
                .AcceptAnyLanguage()
                .Use("POST", null));
        }

        [Fact]
        public void prop_Content()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.Content)
                            .IsNotDecorated()
                            .IsAutoProperty<IHttpContent>()
                            .Result);
        }

        [Fact]
        public void prop_Request()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.Request)
                            .IsNotDecorated()
                            .TypeIs<HttpRequestDefinition>()
                            .ArgumentNullException()
                            .Result);
        }
    }
}