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

    public sealed class MaintenanceControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MaintenanceController>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsDecoratedWith<InternalServerErrorAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MaintenanceController());
        }

        [Fact]
        public void op_Register_RouteCollection()
        {
            var routes = new RouteCollection();

            var controller = (IRegisterRoutes)new MaintenanceController();
            controller.Register(routes);

            var route = (Route)routes["Maintenance"];

            Assert.Equal("{*url}", route.Url);
        }

        [Fact]
        public void op_Register_RouteCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new MaintenanceController() as IRegisterRoutes).Register(null));
        }

        [Fact]
        public void op_Representation()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.ServiceUnavailable)
                .Verifiable();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            var controller = new MaintenanceController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            Assert.IsAssignableFrom<ViewResult>(controller.HtmlRepresentation());

            context.VerifyAll();
        }
    }
}