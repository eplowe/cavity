namespace Cavity.Web.Mvc
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using Cavity.IO;
    using Moq;
    using Xunit;

    public sealed class ContentMD5AttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ContentMD5Attribute>()
                            .DerivesFrom<ActionFilterAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, false, false)
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ContentMD5Attribute());
        }

        [Fact]
        public void op_OnActionExecuted_ActionExecutedContext()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .Setup(x => x.AppendHeader("Content-MD5", "1B2M2Y8AsgTpgAmY7PhCfg=="))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            using (var stream = new MemoryStream())
            {
                response
                    .SetupGet(x => x.Filter)
                    .Returns(new WrappedStream(stream))
                    .Verifiable();

                new ContentMD5Attribute().OnActionExecuted(new ActionExecutedContext
                                                               {
                                                                   HttpContext = context.Object
                                                               });
            }

            response.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuted_ActionExecutedContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentMD5Attribute().OnActionExecuted(null));
        }

        [Fact]
        public void op_OnActionExecuting_FilterExecutedContext()
        {
            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupSet(x => x.Response.BufferOutput = true)
                .Verifiable();
            context
                .SetupSet(x => x.Response.Filter = It.IsAny<WrappedStream>())
                .Verifiable();

            using (var stream = new MemoryStream())
            {
                context
                    .SetupGet(x => x.Response.Filter)
                    .Returns(stream)
                    .Verifiable();

                new ContentMD5Attribute().OnActionExecuting(new ActionExecutingContext
                                                                {
                                                                    HttpContext = context.Object
                                                                });
            }

            context.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuting_FilterExecutedContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ContentMD5Attribute().OnActionExecuting(null));
        }
    }
}