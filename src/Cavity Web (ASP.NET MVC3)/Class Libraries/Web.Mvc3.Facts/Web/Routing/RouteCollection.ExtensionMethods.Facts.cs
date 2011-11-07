namespace Cavity.Web.Routing
{
    using System;
    using System.Web.Routing;
    using Cavity.Web.Mvc;
    using Xunit;

    public sealed class RouteCollectionExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(RouteCollectionExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_RegisterOfT_RouteCollection()
        {
            var routes = new RouteCollection();

            routes.Register<DummyController>();

            var route = (Route)routes["Dummy"];

            Assert.Equal("example", route.Url);
        }

        [Fact]
        public void op_Register_RouteCollection_IEnumerableOfType()
        {
            var routes = new RouteCollection();

            routes.Register(new[]
            {
                typeof(DummyController)
            });

            var route = (Route)routes["Dummy"];

            Assert.Equal("example", route.Url);
        }

        [Fact]
        public void op_Register_RouteCollection_IEnumerableOfTypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RouteCollection().Register(null as Type[]));
        }

        [Fact]
        public void op_Register_RouteCollection_Type()
        {
            var routes = new RouteCollection();

            routes.Register(typeof(DummyController));

            var route = (Route)routes["Dummy"];

            Assert.Equal("example", route.Url);
        }

        [Fact]
        public void op_Register_RouteCollection_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RouteCollection().Register(null as Type));
        }

        [Fact]
        public void op_Register_RouteCollection_TypeNull_bool()
        {
            Assert.Throws<ArgumentNullException>(() => new RouteCollection().Register(null, false));
        }

        [Fact]
        public void op_Register_RouteCollection_Type_bool()
        {
            var routes = new RouteCollection();

            routes.Register(typeof(DummyController), false);

            var route = (Route)routes["Dummy"];

            Assert.Equal("example", route.Url);
        }
    }
}