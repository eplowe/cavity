namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Net;
    using Moq;
    using Xunit;

    public sealed class HttpExpectationsFacts
    {
        [Fact]
        public void IRequestAcceptContent_op_AcceptAnyContent()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent();

            Assert.True(obj.Request.Headers.Contains("Accept", "*/*"));
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
        public void IRequestAcceptLanguage_op_AcceptAnyLanguage()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage();

            Assert.False(obj.Request.Headers.Contains("Accept-Language", "*/*"));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_CultureInfo()
        {
            const string value = "en-GB";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptLanguage(new CultureInfo(value));

            Assert.True(obj.Request.Headers.Contains("Accept-Language", value));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_CultureInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                           .AcceptAnyContent()
                                                           .AcceptLanguage(null as CultureInfo));
        }

        [Fact]
        public void IRequestAcceptLanguage_op_AcceptLanguage_string()
        {
            const string expected = "en-gb, en-US;q=0.8, en;q=0.7";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptLanguage(expected);

            var actual = obj.Request.Headers["Accept-Language"];

            Assert.Equal(expected, actual);
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
        public void IRequestMethod_op_Delete()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Delete();

            Assert.Equal("DELETE", obj.Request.Method);
        }

        [Fact]
        public void IRequestMethod_op_Get()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get();

            Assert.Equal("GET", obj.Request.Method);
        }

        [Fact]
        public void IRequestMethod_op_Head()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Head();

            Assert.Equal("HEAD", obj.Request.Method);
        }

        [Fact]
        public void IRequestMethod_op_Options()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Options();

            Assert.Equal("OPTIONS", obj.Request.Method);
        }

        [Fact]
        public void IRequestMethod_op_Post_IHttpContent()
        {
            var content = new Mock<IHttpContent>().Object;
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Post(content);

            Assert.Equal("POST", obj.Request.Method);
            Assert.Same(content, obj.Content);
        }

        [Fact]
        public void IRequestMethod_op_Put_IHttpContent()
        {
            var content = new Mock<IHttpContent>().Object;
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Put(content);

            Assert.Equal("PUT", obj.Request.Method);
            Assert.Same(content, obj.Content);
        }

        [Fact]
        public void IRequestMethod_op_UseDelete_stringTrace_IHttpContent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                                 .AcceptAnyContent()
                                                                 .AcceptAnyLanguage()
                                                                 .Use("DELETE", new Mock<IHttpContent>().Object));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringGet_IHttpContent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                                 .AcceptAnyContent()
                                                                 .AcceptAnyLanguage()
                                                                 .Use("GET", new Mock<IHttpContent>().Object));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringHead_IHttpContent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                                 .AcceptAnyContent()
                                                                 .AcceptAnyLanguage()
                                                                 .Use("HEAD", new Mock<IHttpContent>().Object));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringOptions_IHttpContent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                                 .AcceptAnyContent()
                                                                 .AcceptAnyLanguage()
                                                                 .Use("OPTIONS", new Mock<IHttpContent>().Object));
        }

        [Fact]
        public void IRequestMethod_op_Use_stringTrace_IHttpContent()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpExpectations.RequestUri("http://example.com/")
                                                                 .AcceptAnyContent()
                                                                 .AcceptAnyLanguage()
                                                                 .Use("TRACE", new Mock<IHttpContent>().Object));
        }

        [Fact]
        public void IRequestMethod_op_Use_string_IHttpContent()
        {
            const string method = "POST";
            var content = new Mock<IHttpContent>().Object;
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Use(method, content);

            Assert.Equal(method, obj.Request.Method);
            Assert.Same(content, obj.Content);
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
        public void IResponseStatus_op_HasCacheControlNone()
        {
            const string expected = "no-cache";

            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlNone();

            var actual = (HttpResponseCacheControlTest)obj
                                                           .Expectations
                                                           .Where(x => typeof(HttpResponseCacheControlTest) == x.GetType())
                                                           .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_HasCacheControlPrivate()
        {
            const string expected = "private";

            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPrivate();

            var actual = (HttpResponseCacheControlTest)obj
                                                           .Expectations
                                                           .Where(x => typeof(HttpResponseCacheControlTest) == x.GetType())
                                                           .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_HasCacheControlPublic()
        {
            const string expected = "public";

            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic();

            var actual = (HttpResponseCacheControlTest)obj
                                                           .Expectations
                                                           .Where(x => typeof(HttpResponseCacheControlTest) == x.GetType())
                                                           .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_HasCacheControl_string()
        {
            const string expected = "public";

            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControl(expected);

            var actual = (HttpResponseCacheControlTest)obj
                                                           .Expectations
                                                           .Where(x => typeof(HttpResponseCacheControlTest) == x.GetType())
                                                           .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact(Skip = "need to decide what to do")]
        public void IResponseStatus_op_HasCacheControl_stringEmpty()
        {
            Assert.True(false);
        }

        [Fact(Skip = "need to decide what to do")]
        public void IResponseStatus_op_HasCacheControl_stringNull()
        {
            Assert.True(false);
        }

        [Fact]
        public void IResponseStatus_op_HasContentLanguage_CultureInfo()
        {
            const string expected = "en-GB";
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithLastModified()
                                            .HasContentLanguage(new CultureInfo(expected));

            var actual = (HttpResponseContentLanguageTest)obj
                                                              .Expectations
                                                              .Where(x => typeof(HttpResponseContentLanguageTest) == x.GetType())
                                                              .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_HasContentLanguage_string()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithLastModified()
                                            .HasContentLanguage("en");

            const string expected = "en";
            var actual = (HttpResponseContentLanguageTest)obj
                                                              .Expectations
                                                              .Where(x => typeof(HttpResponseContentLanguageTest) == x.GetType())
                                                              .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_HasContentMD5()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithLastModified()
                                            .HasContentLanguage("en")
                                            .HasContentMD5();

            Assert.NotNull(obj
                               .Expectations
                               .Where(x => typeof(HttpResponseContentMD5Test) == x.GetType())
                               .FirstOrDefault());
        }

        [Fact]
        public void IResponseStatus_op_IgnoreCacheConditionals()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic();

            var expected = obj.Expectations.Count;

            obj = (HttpExpectations)(obj as IResponseCacheConditionals).IgnoreCacheConditionals();

            var actual = obj.Expectations.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IResponseStatus_op_IgnoreCacheControl()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK);

            var expected = obj.Expectations.Count;

            obj = (HttpExpectations)(obj as IResponseCacheControl).IgnoreCacheControl();

            var actual = obj.Expectations.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IResponseStatus_op_IgnoreContentLanguage()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .IgnoreCacheConditionals();

            var expected = obj.Expectations.Count;

            obj = (HttpExpectations)(obj as IResponseContentLanguage).IgnoreContentLanguage();

            var actual = obj.Expectations.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IResponseStatus_op_IgnoreContentMD5()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .IgnoreCacheConditionals()
                                            .IgnoreContentLanguage();

            var expected = obj.Expectations.Count;

            obj = (HttpExpectations)(obj as IResponseContentMD5).IgnoreContentMD5();

            var actual = obj.Expectations.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IResponseStatus_op_Is_HttpStatusCode()
        {
            const HttpStatusCode expected = HttpStatusCode.OK;

            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(expected);

            var actual = (HttpStatusCodeTest)obj
                                                 .Expectations
                                                 .Where(x => typeof(HttpStatusCodeTest) == x.GetType())
                                                 .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_WithEtag()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithEtag();

            const HttpResponseHeader expected = HttpResponseHeader.ETag;
            var actual = (HttpResponseHeaderTest)obj
                                                     .Expectations
                                                     .Where(x => typeof(HttpResponseHeaderTest) == x.GetType())
                                                     .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_WithExpires()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithExpires();

            const HttpResponseHeader expected = HttpResponseHeader.Expires;
            var actual = (HttpResponseHeaderTest)obj
                                                     .Expectations
                                                     .Where(x => typeof(HttpResponseHeaderTest) == x.GetType())
                                                     .First();

            Assert.Equal(expected, actual.Expected);
        }

        [Fact]
        public void IResponseStatus_op_WithLastModified()
        {
            var obj = (HttpExpectations)HttpExpectations.RequestUri("http://example.com/")
                                            .AcceptAnyContent()
                                            .AcceptAnyLanguage()
                                            .Get()
                                            .Is(HttpStatusCode.OK)
                                            .HasCacheControlPublic()
                                            .WithLastModified();

            const HttpResponseHeader expected = HttpResponseHeader.LastModified;
            var actual = (HttpResponseHeaderTest)obj
                                                     .Expectations
                                                     .Where(x => typeof(HttpResponseHeaderTest) == x.GetType())
                                                     .First();

            Assert.Equal(expected, actual.Expected);
        }

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
        public void prop_Content()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.Content)
                            .IsNotDecorated()
                            .IsAutoProperty<IHttpContent>()
                            .Result);
        }

        [Fact]
        public void prop_Expectations()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.Expectations)
                            .IsNotDecorated()
                            .TypeIs<ICollection<ITestHttpExpectation>>()
                            .Result);
        }

        [Fact]
        public void prop_Request()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.Request)
                            .IsNotDecorated()
                            .TypeIs<IWebRequest>()
                            .ArgumentNullException()
                            .Result);
        }
    }
}