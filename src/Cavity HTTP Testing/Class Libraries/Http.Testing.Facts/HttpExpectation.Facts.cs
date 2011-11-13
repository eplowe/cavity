namespace Cavity.Net
{
    using System;
    using System.Net;
    using Cavity.Security.Cryptography;
    using Xunit;

    public sealed class HttpExpectationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpExpectation>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpExpectation());
        }

        [Fact]
        public void op_GetRequest_HttpWebRequestEmpty_CookieContainer()
        {
            Assert.Throws<InvalidOperationException>(() => HttpExpectation.GetRequest(new HttpRequest(), new CookieContainer()));
        }

        [Fact]
        public void op_GetRequest_HttpWebRequestNull_CookieContainer()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectation.GetRequest(null, new CookieContainer()));
        }

        [Fact]
        public void op_GetRequest_HttpWebRequest_CookieContainer()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("GET", "http://example.com/"), 
                Headers = new HttpHeaderDictionary()
            };

            request.Headers["Accept"] = "*/*";
            request.Headers["Accept-Charset"] = "*";
            request.Headers["Accept-Encoding"] = "*";
            request.Headers["Accept-Language"] = "*";
            request.Headers["Authorization"] = "credentials";
            request.Headers["Cache-Control"] = "no-cache";
            request.Headers["Date"] = "Tue, 15 Nov 1994 08:12:31 GMT";
            request.Headers["Expires"] = "Thu, 01 Dec 1994 16:00:00 GMT";
            request.Headers["From"] = "webmaster@example.com";
            request.Headers["Host"] = "example.com";
            request.Headers["If-Match"] = "*";
            request.Headers["If-Modified-Since"] = "Sat, 29 Oct 1994 19:43:31 GMT";
            request.Headers["If-None-Match"] = "*";
            request.Headers["If-Range"] = "*";
            request.Headers["If-Unmodified-Since"] = "Sat, 29 Oct 1994 19:43:31 GMT";
            request.Headers["Last-Modified"] = "Tue, 15 Nov 1994 12:45:26 GMT";
            request.Headers["Pragma"] = "name=value";
            request.Headers["Proxy-Authorization"] = "credentials";
            request.Headers["Range"] = "bytes=0-7";
            request.Headers["Referer"] = "http://example.net/";
            request.Headers["Range"] = "bytes=0-7";
            request.Headers["TE"] = "deflate";
            request.Headers["Transfer-Encoding"] = "chunked";
            request.Headers["Upgrade"] = "HTTP/2.0";
            request.Headers["User-Agent"] = "Example Facts";
            request.Headers["Vary"] = "*";
            request.Headers["Via"] = "1.0 fred, 1.1 nowhere.com (Apache/1.1)";
            request.Headers["Warning"] = "199 Miscellaneous warning";

            var actual = HttpExpectation.GetRequest(request, cookies);

            Assert.False(actual.AllowAutoRedirect);
            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);

            Assert.Equal(request.Headers["Accept"], actual.Accept);
            Assert.Equal(request.Headers["Accept-Charset"], actual.Headers[HttpRequestHeader.AcceptCharset]);
            Assert.Equal(request.Headers["Accept-Encoding"], actual.Headers[HttpRequestHeader.AcceptEncoding]);
            Assert.Equal(request.Headers["Accept-Language"], actual.Headers[HttpRequestHeader.AcceptLanguage]);
            Assert.Equal(request.Headers["Authorization"], actual.Headers[HttpRequestHeader.Authorization]);
            Assert.Equal(request.Headers["Cache-Control"], actual.Headers[HttpRequestHeader.CacheControl]);
            Assert.Equal(request.Headers["Date"], actual.Headers[HttpRequestHeader.Date]);
            Assert.Equal(request.Headers["Expires"], actual.Headers[HttpRequestHeader.Expires]);
            Assert.Equal(request.Headers["From"], actual.Headers[HttpRequestHeader.From]);
            Assert.Null(actual.Headers[HttpRequestHeader.Host]);
            Assert.Equal(request.Headers["If-Match"], actual.Headers[HttpRequestHeader.IfMatch]);
            Assert.Equal(request.Headers["If-Modified-Since"], actual.Headers[HttpRequestHeader.IfModifiedSince]);
            Assert.Equal(request.Headers["If-None-Match"], actual.Headers[HttpRequestHeader.IfNoneMatch]);
            Assert.Equal(request.Headers["If-Range"], actual.Headers[HttpRequestHeader.IfRange]);
            Assert.Equal(request.Headers["If-Unmodified-Since"], actual.Headers[HttpRequestHeader.IfUnmodifiedSince]);
            Assert.Equal(request.Headers["Last-Modified"], actual.Headers[HttpRequestHeader.LastModified]);
            Assert.Equal(request.Headers["Pragma"], actual.Headers[HttpRequestHeader.Pragma]);
            Assert.Equal(request.Headers["Proxy-Authorization"], actual.Headers[HttpRequestHeader.ProxyAuthorization]);
            Assert.Null(actual.Headers[HttpRequestHeader.Range]);
            Assert.Equal(request.Headers["Referer"], actual.Headers[HttpRequestHeader.Referer]);
            Assert.Equal(request.Headers["TE"], actual.Headers[HttpRequestHeader.Te]);
            Assert.Equal(request.Headers["Upgrade"], actual.Headers[HttpRequestHeader.Upgrade]);
            Assert.Equal(request.Headers["User-Agent"], actual.Headers[HttpRequestHeader.UserAgent]);
            Assert.Equal(request.Headers["Via"], actual.Headers[HttpRequestHeader.Via]);
            Assert.Equal(request.Headers["Warning"], actual.Headers[HttpRequestHeader.Warning]);
        }

        [Fact]
        public void op_GetRequest_HttpWebRequest_CookieContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectation.GetRequest(new HttpRequest(), null));
        }

        [Fact]
        public void op_GetRequest_HttpWebRequest_CookieContainer_whenEntity()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("PUT", "http://example.com/entity"), 
                Headers = new HttpHeaderDictionary(), 
                Body = new TextBody("example")
            };

            request.Headers["Content-Encoding"] = "gzip";
            request.Headers["Content-Language"] = "en";
            request.Headers["Content-Length"] = "7";
            request.Headers["Content-Location"] = "/other";
            request.Headers["Content-MD5"] = MD5Hash.Compute("example");
            request.Headers["Content-Range"] = "0-6/99";
            request.Headers["Content-Type"] = "text/plain";

            var actual = HttpExpectation.GetRequest(request, cookies);

            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);

            Assert.Equal(request.Headers["Content-Encoding"], actual.Headers[HttpRequestHeader.ContentEncoding]);
            Assert.Equal(request.Headers["Content-Language"], actual.Headers[HttpRequestHeader.ContentLanguage]);
            Assert.Null(actual.Headers[HttpRequestHeader.ContentLength]);
            Assert.Equal(request.Headers["Content-Location"], actual.Headers[HttpRequestHeader.ContentLocation]);
            Assert.Equal(request.Headers["Content-MD5"], actual.Headers[HttpRequestHeader.ContentMd5]);
            Assert.Equal(request.Headers["Content-Range"], actual.Headers[HttpRequestHeader.ContentRange]);
            Assert.Equal(request.Headers["Content-Type"], actual.Headers[HttpRequestHeader.ContentType]);
        }

        [Fact]
        public void op_GetRequest_HttpWebRequest_CookieContainer_whenOnlyLine()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("GET", "http://example.com/")
            };

            var actual = HttpExpectation.GetRequest(request, cookies);

            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);
        }

        [Fact]
        public void op_GetRequest_HttpWebRequest_CookieContainer_whenTrace()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("TRACE", "http://example.com/"), 
                Headers = new HttpHeaderDictionary()
            };

            request.Headers["Max-Forwards"] = "3";

            var actual = HttpExpectation.GetRequest(request, cookies);

            Assert.Equal((string)request.Line.Method, actual.Method);
            Assert.Equal((string)request.Line.RequestUri, actual.Address.AbsoluteUri);

            Assert.Same(cookies, actual.CookieContainer);

            Assert.Equal(request.Headers["Max-Forwards"], actual.Headers[HttpRequestHeader.MaxForwards]);
        }

        [Fact]
        public void op_GetResponse_HttpWebRequest_CookieContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpExpectation.GetResponse(new HttpRequest(), null));
        }

        [Fact]
        public void op_GetResponse_HttpWebRequest_CookieContainer_whenNotFound()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("GET", "http://www.alan-dean.com/example")
            };

            using (var response = HttpExpectation.GetResponse(request, cookies))
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public void op_GetResponse_HttpWebRequest_CookieContainer_whenOnlyLine()
        {
            var cookies = new CookieContainer();
            var request = new HttpRequest
            {
                Line = new HttpRequestLine("GET", "http://www.alan-dean.com/")
            };

            using (var response = HttpExpectation.GetResponse(request, cookies))
            {
                Assert.Equal(HttpStatusCode.SeeOther, response.StatusCode);
                Assert.Equal("http://www.alan-dean.com/about", response.Headers[HttpResponseHeader.Location]);
            }
        }

        [Fact]
        public void op_Verify_CookieContainer()
        {
            var cookies = new CookieContainer();
            var expectation = new HttpExpectation
            {
                Exchange = new HttpExchange
                {
                    Request = new HttpRequest
                    {
                        Line = new HttpRequestLine("GET", "http://www.alan-dean.com/")
                    }, 
                    Response = new HttpResponse
                    {
                        Line = new HttpStatusLine(HttpStatusCode.SeeOther), 
                        Headers = new HttpHeaderDictionary
                        {
                            new HttpHeader("Location", "http://www.alan-dean.com/about")
                        }
                    }
                }
            };

            Assert.True(expectation.Verify(cookies));
        }

        [Fact]
        public void op_Verify_CookieContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpExpectation().Verify(null));
        }

        [Fact]
        public void op_Verify_CookieContainer_whenNullExchange()
        {
            Assert.Throws<InvalidOperationException>(() => new HttpExpectation().Verify(new CookieContainer()));
        }

        [Fact]
        public void op_Verify_CookieContainer_whenWrongLocation()
        {
            var cookies = new CookieContainer();
            var expectation = new HttpExpectation
            {
                Exchange = new HttpExchange
                {
                    Request = new HttpRequest
                    {
                        Line = new HttpRequestLine("GET", "http://www.alan-dean.com/")
                    }, 
                    Response = new HttpResponse
                    {
                        Line = new HttpStatusLine(HttpStatusCode.SeeOther), 
                        Headers = new HttpHeaderDictionary
                        {
                            new HttpHeader("Location", "http://www.alan-dean.com/example")
                        }
                    }
                }
            };

            Assert.Throws<HttpTestException>(() => expectation.Verify(cookies));
        }

        [Fact]
        public void op_Verify_CookieContainer_whenWrongStatus()
        {
            var cookies = new CookieContainer();
            var expectation = new HttpExpectation
            {
                Exchange = new HttpExchange
                {
                    Request = new HttpRequest
                    {
                        Line = new HttpRequestLine("GET", "http://www.alan-dean.com/")
                    }, 
                    Response = new HttpResponse
                    {
                        Line = new HttpStatusLine(HttpStatusCode.OK)
                    }
                }
            };

            Assert.Throws<HttpTestException>(() => expectation.Verify(cookies));
        }

        [Fact]
        public void prop_Exchange()
        {
            Assert.True(new PropertyExpectations<HttpExpectation>(x => x.Exchange)
                            .IsAutoProperty<HttpExchange>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}