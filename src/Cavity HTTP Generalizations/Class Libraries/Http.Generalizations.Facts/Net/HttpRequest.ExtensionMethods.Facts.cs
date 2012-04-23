namespace Cavity.Net
{
    using System;
    using System.Net;

    using Xunit;

    public sealed class HttpRequestExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpRequestExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ToWebRequest_HttpRequest()
        {
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("GET", "http://example.com/"), 
                                  Headers = new HttpHeaderDictionary()
                              };

            Assert.NotNull(request.ToWebRequest().CookieContainer);
        }

        [Fact]
        public void op_ToWebRequest_HttpRequestEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new HttpRequest().ToWebRequest());
        }

        [Fact]
        public void op_ToWebRequest_HttpRequestEmpty_CookieContainer()
        {
            var cookies = new CookieContainer();

            Assert.Throws<InvalidOperationException>(() => new HttpRequest().ToWebRequest(cookies));
        }

        [Fact]
        public void op_ToWebRequest_HttpRequestNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequest).ToWebRequest());
        }

        [Fact]
        public void op_ToWebRequest_HttpRequestNull_CookieContainer()
        {
            var cookies = new CookieContainer();

            Assert.Throws<ArgumentNullException>(() => (null as HttpRequest).ToWebRequest(cookies));
        }

        [Fact]
        public void op_ToWebRequest_HttpRequest_CookieContainer()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("GET", "http://example.com/"), 
                                  Headers = new HttpHeaderDictionary()
                              };

            request.Headers[HttpGeneralHeaders.CacheControl] = "no-cache";
            request.Headers[HttpGeneralHeaders.Date] = "Tue, 15 Nov 1994 08:12:31 GMT";
            request.Headers[HttpGeneralHeaders.Pragma] = "name=value";
            request.Headers[HttpGeneralHeaders.TransferEncoding] = "chunked";
            request.Headers[HttpGeneralHeaders.Upgrade] = "HTTP/2.0";
            request.Headers[HttpGeneralHeaders.Via] = "1.0 fred, 1.1 nowhere.com (Apache/1.1)";
            request.Headers[HttpGeneralHeaders.Warning] = "199 Miscellaneous warning";

            request.Headers[HttpEntityHeaders.Expires] = "Thu, 01 Dec 1994 16:00:00 GMT";
            request.Headers[HttpEntityHeaders.LastModified] = "Tue, 15 Nov 1994 12:45:26 GMT";

            request.Headers[HttpRequestHeaders.Accept] = "*/*";
            request.Headers[HttpRequestHeaders.AcceptCharset] = "*";
            request.Headers[HttpRequestHeaders.AcceptEncoding] = "*";
            request.Headers[HttpRequestHeaders.AcceptLanguage] = "*";
            request.Headers[HttpRequestHeaders.Authorization] = "credentials";
            request.Headers[HttpRequestHeaders.From] = "webmaster@example.com";
            request.Headers[HttpRequestHeaders.Host] = "example.com";
            request.Headers[HttpRequestHeaders.IfMatch] = "*";
            request.Headers[HttpRequestHeaders.IfModifiedSince] = "Sat, 29 Oct 1994 19:43:31 GMT";
            request.Headers[HttpRequestHeaders.IfNoneMatch] = "*";
            request.Headers[HttpRequestHeaders.IfRange] = "*";
            request.Headers[HttpRequestHeaders.IfUnmodifiedSince] = "Sat, 29 Oct 1994 19:43:31 GMT";
            request.Headers[HttpRequestHeaders.ProxyAuthorization] = "credentials";
            request.Headers[HttpRequestHeaders.Range] = "bytes=0-7";
            request.Headers[HttpRequestHeaders.Referer] = "http://example.net/";
            request.Headers[HttpRequestHeaders.Range] = "bytes=0-7";
            request.Headers[HttpRequestHeaders.TE] = "deflate";
            request.Headers[HttpRequestHeaders.UserAgent] = "Example Facts";
            request.Headers[HttpRequestHeaders.Vary] = "*";

            var actual = request.ToWebRequest(cookies);

            Assert.False(actual.AllowAutoRedirect);
            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);

            Assert.Equal(request.Headers[HttpGeneralHeaders.CacheControl], actual.Headers[HttpRequestHeader.CacheControl]);
#if NET20 || NET35
#else
            Assert.Equal(request.Headers[HttpGeneralHeaders.Date], actual.Headers[HttpRequestHeader.Date]);
#endif
            Assert.Equal(request.Headers[HttpGeneralHeaders.Pragma], actual.Headers[HttpRequestHeader.Pragma]);
            Assert.Equal(request.Headers[HttpGeneralHeaders.Upgrade], actual.Headers[HttpRequestHeader.Upgrade]);
            Assert.Equal(request.Headers[HttpGeneralHeaders.Via], actual.Headers[HttpRequestHeader.Via]);
            Assert.Equal(request.Headers[HttpGeneralHeaders.Warning], actual.Headers[HttpRequestHeader.Warning]);

            Assert.Equal(request.Headers[HttpEntityHeaders.Expires], actual.Headers[HttpRequestHeader.Expires]);
            Assert.Equal(request.Headers[HttpEntityHeaders.LastModified], actual.Headers[HttpRequestHeader.LastModified]);

            Assert.Equal(request.Headers[HttpRequestHeaders.Accept], actual.Accept);
            Assert.Equal(request.Headers[HttpRequestHeaders.AcceptCharset], actual.Headers[HttpRequestHeader.AcceptCharset]);
            Assert.Equal(request.Headers[HttpRequestHeaders.AcceptEncoding], actual.Headers[HttpRequestHeader.AcceptEncoding]);
            Assert.Equal(request.Headers[HttpRequestHeaders.AcceptLanguage], actual.Headers[HttpRequestHeader.AcceptLanguage]);
            Assert.Equal(request.Headers[HttpRequestHeaders.Authorization], actual.Headers[HttpRequestHeader.Authorization]);
            Assert.Equal(request.Headers[HttpRequestHeaders.From], actual.Headers[HttpRequestHeader.From]);
            Assert.Null(actual.Headers[HttpRequestHeaders.Host]);
            Assert.Equal(request.Headers[HttpRequestHeaders.IfMatch], actual.Headers[HttpRequestHeader.IfMatch]);
            Assert.Equal(request.Headers[HttpRequestHeaders.IfModifiedSince], actual.Headers[HttpRequestHeader.IfModifiedSince]);
            Assert.Equal(request.Headers[HttpRequestHeaders.IfNoneMatch], actual.Headers[HttpRequestHeader.IfNoneMatch]);
            Assert.Equal(request.Headers[HttpRequestHeaders.IfRange], actual.Headers[HttpRequestHeader.IfRange]);
            Assert.Equal(request.Headers[HttpRequestHeaders.IfUnmodifiedSince], actual.Headers[HttpRequestHeader.IfUnmodifiedSince]);
            Assert.Equal(request.Headers[HttpRequestHeaders.ProxyAuthorization], actual.Headers[HttpRequestHeader.ProxyAuthorization]);
            Assert.Null(actual.Headers[HttpRequestHeader.Range]);
            Assert.Equal(request.Headers[HttpRequestHeaders.Referer], actual.Headers[HttpRequestHeader.Referer]);
            Assert.Equal(request.Headers[HttpRequestHeaders.TE], actual.Headers[HttpRequestHeader.Te]);
            Assert.Equal(request.Headers[HttpRequestHeaders.UserAgent], actual.Headers[HttpRequestHeader.UserAgent]);
        }

        [Fact]
        public void op_ToWebRequest_HttpRequest_CookieContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().ToWebRequest(null));
        }

        [Fact]
        public void op_ToWebRequest_HttpRequest_CookieContainer_whenOnlyLine()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("GET", "http://example.com/")
                              };

            var actual = request.ToWebRequest(cookies);

            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);
        }

        [Fact]
        public void op_ToWebRequest_HttpRequest_CookieContainer_whenTrace()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("TRACE", "http://example.com/"), 
                                  Headers = new HttpHeaderDictionary()
                              };

            request.Headers["Max-Forwards"] = "3";

            var actual = request.ToWebRequest(cookies);

            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);

            Assert.Equal(request.Headers["Max-Forwards"], actual.Headers[HttpRequestHeader.MaxForwards]);
        }

        [Fact]
        public void op_ToWebResponse_HttpRequestEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new HttpRequest().ToWebResponse());
        }

        [Fact]
        public void op_ToWebResponse_HttpRequestEmpty_CookieContainer()
        {
            var cookies = new CookieContainer();

            Assert.Throws<InvalidOperationException>(() => new HttpRequest().ToWebResponse(cookies));
        }

        [Fact]
        public void op_ToWebResponse_HttpRequestNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HttpRequest).ToWebResponse());
        }

        [Fact]
        public void op_ToWebResponse_HttpRequestNull_CookieContainer()
        {
            var cookies = new CookieContainer();

            Assert.Throws<ArgumentNullException>(() => (null as HttpRequest).ToWebResponse(cookies));
        }

        [Fact]
        public void op_ToWebResponse_HttpRequest_CookieContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest().ToWebResponse(null));
        }

        [Fact]
        public void op_ToWebResponse_HttpRequest_CookieContainer_whenNotFound()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("GET", "http://www.alan-dean.com/{0}".FormatWith(AlphaDecimal.Random()))
                              };

            using (var response = request.ToWebResponse(cookies))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public void op_ToWebResponse_HttpRequest_CookieContainer_whenOnlyLine()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
                              {
                                  Line = new HttpRequestLine("GET", "http://www.alan-dean.com/")
                              };

            using (var response = request.ToWebResponse(cookies))
            {
                Assert.Equal(HttpStatusCode.SeeOther, response.StatusCode);
                Assert.Equal("http://www.alan-dean.com/about", response.Headers[HttpResponseHeader.Location]);
            }
        }
    }
}