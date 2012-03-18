namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Cavity.Web.Routing;

    using Moq;

    using Xunit;

    public sealed class NotFoundControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NotFoundController>()
                            .DerivesFrom<Controller>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsDecoratedWith<InternalServerErrorAttribute>()
                            .Implements<IRegisterRoutes>()
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
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.NotFound)
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            var controller = new NotFoundController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            Assert.IsType<ViewResult>(controller.HtmlRepresentation());
            Assert.Equal("The requested resource was not found.", controller.ViewBag.Message);

            response.VerifyAll();
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollection()
        {
            var routes = new RouteCollection();

            var controller = (IRegisterRoutes)new NotFoundController();
            controller.Register(routes);

            var route = (Route)routes["404"];

            Assert.Equal("{*url}", route.Url);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollectionNull()
        {
            var controller = (IRegisterRoutes)new NotFoundController();

            Assert.Throws<ArgumentNullException>(() => controller.Register(null));
        }
    }
}