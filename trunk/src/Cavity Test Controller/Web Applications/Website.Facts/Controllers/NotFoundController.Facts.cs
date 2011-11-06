namespace Cavity.Controllers
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class NotFoundControllerFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<NotFoundController>()
                .DerivesFrom<Controller>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new NotFoundController());
        }

        [Fact]
        public void op_HtmlRepresentation()
        {
            var response = new Mock<HttpResponseBase>();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.NotFound)
                .Verifiable();

            var context = new Mock<HttpContextBase>();
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            var controller = new NotFoundController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            Assert.IsType<ViewResult>(controller.HtmlRepresentation());

            response.VerifyAll();
        }
    }
}