namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Moq;

    using Xunit;

    public sealed class StatusAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StatusAttribute>()
                            .DerivesFrom<ActionFilterAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsDecoratedWith<AttributeUsageAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor_HttpStatusCode()
        {
            Assert.NotNull(new StatusAttribute(HttpStatusCode.Accepted));
        }

        [Fact]
        public void op_OnActionExecuted_ActionExecutedContext()
        {
            const HttpStatusCode code = HttpStatusCode.PreconditionFailed;

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.StatusCode = (int)code)
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new StatusAttribute(code).OnActionExecuted(new ActionExecutedContext
                                                           {
                                                               HttpContext = context.Object
                                                           });

            context.VerifyAll();
        }

        [Fact]
        public void op_OnActionExecuted_ActionExecutedContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StatusAttribute(HttpStatusCode.Accepted).OnActionExecuted(null));
        }

        [Fact]
        public void prop_Code()
        {
            Assert.True(new PropertyExpectations<StatusAttribute>(x => x.Code)
                            .TypeIs<HttpStatusCode>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}