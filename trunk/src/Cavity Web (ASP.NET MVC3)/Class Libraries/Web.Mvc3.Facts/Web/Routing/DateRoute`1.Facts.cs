namespace Cavity.Web.Routing
{
    using System;
    using System.Web;
    using System.Web.Routing;

    using Cavity.Web.Mvc;

    using Moq;

    using Xunit;

    public sealed class DateRouteOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DateRoute<DummyController>>()
                            .DerivesFrom<RouteBase>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_stringEmpty_string()
        {
            Assert.NotNull(new DateRoute<DummyController>(string.Empty, "Representation"));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new DateRoute<DummyController>(".html", "HtmlRepresentation"));
        }

        [Fact]
        public void op_GetRouteData_HttpContextBase()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .SetupGet(x => x.Path)
                .Returns("/1999-12-31")
                .Verifiable();
            request
                .SetupGet(x => x.AppRelativeCurrentExecutionFilePath).Returns("~/1999-12-31")
                .Verifiable();
            request
                .SetupGet(x => x.PathInfo)
                .Returns(string.Empty)
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();

            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            var actual = route.GetRouteData(context.Object);

            // ReSharper disable PossibleNullReferenceException
            Assert.Equal("1999-12-31", (actual.Route as Route).Url);

            // ReSharper restore PossibleNullReferenceException
            Assert.Equal("Dummy", (string)actual.Values["controller"]);
            Assert.Equal("Representation", (string)actual.Values["action"]);

            request.VerifyAll();
        }

        [Fact]
        public void op_GetRouteData_HttpContextBaseNull()
        {
            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => route.GetRouteData(null));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetRouteData_HttpContextBase_whenNotFound()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .SetupGet(x => x.Path)
                .Returns("/example")
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();

            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            var actual = route.GetRouteData(context.Object);

            Assert.Null(actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_GetVirtualPath_RequestContextNull_RouteValueDictionary()
        {
            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => route.GetVirtualPath(null, new RouteValueDictionary()));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetVirtualPath_RequestContext_RouteValueDictionary()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .SetupGet(x => x.Path)
                .Returns("/1999-12-31")
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();

            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            var actual = route.GetVirtualPath(
                new RequestContext(context.Object, new RouteData()), 
                new RouteValueDictionary());

            Assert.NotNull(actual);

            request.VerifyAll();
        }

        [Fact]
        public void op_GetVirtualPath_RequestContext_RouteValueDictionary_whenInvalidDate()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .SetupGet(x => x.Path)
                .Returns("/1999-02-30")
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Request)
                .Returns(request.Object)
                .Verifiable();

            var route = new DateRoute<DummyController>(string.Empty, "Representation");

            var actual = route.GetVirtualPath(
                new RequestContext(context.Object, new RouteData()), 
                new RouteValueDictionary());

            Assert.Null(actual);

            request.VerifyAll();
        }
    }
}