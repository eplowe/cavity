namespace Cavity
{
    using System;
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
        public void op_RegisterRoutes_RouteCollection()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            Assert.NotNull(routes["404"]);
            Assert.NotNull(routes["Root"]);
            Assert.NotNull(routes["Today"]);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => MvcApplication.RegisterRoutes(null));
        }
    }
}