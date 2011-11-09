namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using Xunit;

    public sealed class NoContentResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NoContentResult>()
                            .DerivesFrom<ActionResult>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new NoContentResult());
        }

        [Fact]
        public void op_ExecuteResult_ControllerContext()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.NoContent)
                .Verifiable();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new NoContentResult().ExecuteResult(new ControllerContext()
            {
                HttpContext = context.Object
            });

            context.VerifyAll();
        }

        [Fact]
        public void op_ExecuteResult_ControllerContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NoContentResult().ExecuteResult(null as ControllerContext));
        }
    }
}