namespace Cavity
{
    using System;
    using System.Net;

    using Cavity.Net;

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