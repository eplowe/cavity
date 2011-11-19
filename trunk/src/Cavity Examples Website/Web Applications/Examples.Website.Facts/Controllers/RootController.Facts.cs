namespace Cavity.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Cavity;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;
    using Xunit;

    public sealed class RootControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RootController>()
                            .DerivesFrom<Controller>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsDecoratedWith<AllowAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RootController());
        }

        [Fact]
        public void op_Redirect()
        {
            var result = (FoundResult)new RootController().Redirect();

            var expected = "/{0}".FormatWith(DateTime.UtcNow.ToXmlString().Substring(0, 10));
            var actual = result.Location;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollection()
        {
            var routes = new RouteCollection();

            var controller = (IRegisterRoutes)new RootController();
            controller.Register(routes);

            var route = (Route)routes["Root"];

            Assert.Equal(string.Empty, route.Url);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollectionNull()
        {
            var controller = (IRegisterRoutes)new RootController();

            Assert.Throws<ArgumentNullException>(() => controller.Register(null));
        }
    }
}