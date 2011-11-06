namespace Cavity
{
    using System.Web;
    using System.Web.Routing;
    using Xunit;

    public sealed class MvcApplicationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MvcApplication>()
                            .DerivesFrom<HttpApplication>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MvcApplication());
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollection_rootRoute()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var route = (Route)routes["Root"];

            Assert.Equal(string.Empty, route.Url);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollection_404Route()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var route = (Route)routes["404"];

            Assert.Equal("*", route.Url);
        }
    }
}