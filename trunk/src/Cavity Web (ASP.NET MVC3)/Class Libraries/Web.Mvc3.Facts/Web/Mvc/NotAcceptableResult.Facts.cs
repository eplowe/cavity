namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class NotAcceptableResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NotAcceptableResult>()
                .DerivesFrom<ActionResult>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new NotAcceptableResult());
        }

        [Fact]
        public void op_ExecuteResult_ControllerContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NotAcceptableResult().ExecuteResult(null));
        }

        [Fact]
        public void op_ExecuteResult_ControllerContext()
        {
            var response = new Mock<HttpResponseBase>();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.NotAcceptable)
                .Verifiable();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new NotAcceptableResult().ExecuteResult(new ControllerContext()
            {
                HttpContext = context.Object
            });

            response.VerifyAll();
        }
    }
}