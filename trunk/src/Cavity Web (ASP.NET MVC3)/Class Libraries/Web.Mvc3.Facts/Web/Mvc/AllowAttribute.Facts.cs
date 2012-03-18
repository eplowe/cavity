namespace Cavity.Web.Mvc
{
    using System;
    using System.IO;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Cavity.IO;

    using Moq;

    using Xunit;

    public sealed class AllowAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AllowAttribute>()
                            .DerivesFrom<ActionFilterAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsDecoratedWith<AttributeUsageAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new AllowAttribute("GET, HEAD, OPTIONS"));
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AllowAttribute("GET, HEAD, OPTIONS").OnActionExecuting(null));
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContext_whenMethodAllowed()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.HttpMethod)
                .Returns("GET")
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();

            var obj = new AllowAttribute("GET, HEAD, OPTIONS");

            obj.OnActionExecuting(new ActionExecutingContext
                                      {
                                          HttpContext = context.Object
                                      });

            request.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContext_whenMethodNotAllowed()
        {
            using (var stream = new MemoryStream())
            {
                var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
                request
                    .SetupGet(x => x.HttpMethod)
                    .Returns("PUT")
                    .Verifiable();

                var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
                response
                    .Setup(x => x.Cache.SetCacheability(HttpCacheability.Public))
                    .Verifiable();
                response
                    .Setup(x => x.Cache.SetExpires(It.IsAny<DateTime>()))
                    .Verifiable();
                response
                    .Setup(x => x.AppendHeader("Allow", "GET, HEAD, OPTIONS"))
                    .Verifiable();
                response
                    .Setup(x => x.Clear())
                    .Verifiable();
                response
                    .Setup(x => x.Filter)
                    .Returns(new WrappedStream(stream))
                    .Verifiable();

                // ReSharper disable AccessToDisposedClosure
                response
                    .SetupSet(x => x.Filter = stream)
                    .Verifiable();

                // ReSharper restore AccessToDisposedClosure
                response
                    .SetupSet(x => x.StatusCode = (int)HttpStatusCode.MethodNotAllowed)
                    .Verifiable();
                response
                    .Setup(x => x.End())
                    .Verifiable();

                var context = new Mock<HttpContextBase>(MockBehavior.Strict);
                context
                    .SetupGet(x => x.Request)
                    .Returns(request.Object)
                    .Verifiable();
                context
                    .SetupGet(x => x.Response)
                    .Returns(response.Object)
                    .Verifiable();

                var obj = new AllowAttribute("GET, HEAD, OPTIONS");

                obj.OnActionExecuting(new ActionExecutingContext
                                          {
                                              HttpContext = context.Object
                                          });

                request.VerifyAll();
                response.VerifyAll();
            }
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContext_whenMethods()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.HttpMethod)
                .Returns("OPTIONS")
                .Verifiable();

            var response = new Mock<HttpResponseBase>();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.Public))
                .Verifiable();
            response
                .Setup(x => x.Cache.SetExpires(It.IsAny<DateTime>()))
                .Verifiable();
            response
                .Setup(x => x.AppendHeader("Allow", "GET, HEAD, OPTIONS"))
                .Verifiable();
            response
                .Setup(x => x.Clear())
                .Verifiable();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.OK)
                .Verifiable();
            response
                .Setup(x => x.End())
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            var obj = new AllowAttribute("GET, HEAD, OPTIONS");

            obj.OnActionExecuting(new ActionExecutingContext
                                      {
                                          HttpContext = context.Object
                                      });

            request.VerifyAll();
            response.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContext_whenMethodsNull()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.HttpMethod)
                .Returns("OPTIONS")
                .Verifiable();

            var response = new Mock<HttpResponseBase>();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.Public))
                .Verifiable();
            response
                .Setup(x => x.Cache.SetExpires(It.IsAny<DateTime>()))
                .Verifiable();
            response
                .Setup(x => x.AppendHeader("Allow", "GET, HEAD, OPTIONS"))
                .Verifiable();
            response
                .Setup(x => x.Clear())
                .Verifiable();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.OK)
                .Verifiable();
            response
                .Setup(x => x.End())
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new AllowAttribute("GET, HEAD, OPTIONS")
                .OnActionExecuting(new ActionExecutingContext
                                       {
                                           HttpContext = context.Object
                                       });

            request.VerifyAll();
            response.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuting_ActionExecutingContext_whenRequestOptions()
        {
            var request = new Mock<HttpRequestBase>();
            request
                .SetupGet(x => x.HttpMethod)
                .Returns("OPTIONS")
                .Verifiable();

            var response = new Mock<HttpResponseBase>();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.Public))
                .Verifiable();
            response
                .Setup(x => x.Cache.SetExpires(It.IsAny<DateTime>()))
                .Verifiable();
            response
                .Setup(x => x.AppendHeader("Allow", "GET, HEAD, OPTIONS"))
                .Verifiable();
            response
                .Setup(x => x.Clear())
                .Verifiable();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.OK)
                .Verifiable();
            response
                .Setup(x => x.End())
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new AllowAttribute("GET, HEAD, OPTIONS")
                .OnActionExecuting(new ActionExecutingContext
                                       {
                                           HttpContext = context.Object
                                       });

            request.VerifyAll();
            response.VerifyAll();
        }

        [Fact]
        public void prop_Methods()
        {
            Assert.True(new PropertyExpectations<AllowAttribute>(x => x.Methods)
                            .IsAutoProperty<string>()
                            .Set("GET, HEAD, OPTIONS, POST")
                            .Result);
        }
    }
}