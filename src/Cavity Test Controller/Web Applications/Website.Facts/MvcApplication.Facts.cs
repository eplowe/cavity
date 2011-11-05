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
        public void op_RegisterRoutes_RouteCollection_defaultRoute()
        {
            var routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);

            var route = (Route)routes["Default"];

            Assert.Equal("{controller}/{action}/{id}", route.Url);
        }
    }
}