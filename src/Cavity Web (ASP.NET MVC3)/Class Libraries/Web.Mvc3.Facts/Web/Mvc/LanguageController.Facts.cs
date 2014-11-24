namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Moq;
    using Xunit;

    public sealed class LanguageControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LanguageController>()
                            .DerivesFrom<Controller>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DerivedLanguageController());
        }

        [Fact]
        public void op_ContentNegotiation_CultureInfo()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                const string location = "/test.fr";

                var headers = new NameValueCollection();
                headers["Accept"] = "*/*, text/*, text/html";

                var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
                request
                    .SetupGet(x => x.Headers)
                    .Returns(headers)
                    .Verifiable();
                request
                    .SetupGet(x => x.Path)
                    .Returns(location)
                    .Verifiable();
                request
                    .SetupGet(x => x.RawUrl)
                    .Returns(location)
                    .Verifiable();

                var context = new Mock<HttpContextBase>(MockBehavior.Strict);
                context
                    .SetupGet(x => x.Request)
                    .Returns(request.Object)
                    .Verifiable();

                var controller = new DerivedLanguageController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var result = (SeeOtherResult)controller.ContentNegotiation(new CultureInfo("fr"));

                Assert.Equal(location + ".html", result.Location);

                request.VerifyAll();
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_ContentNegotiation_CultureInfoInvariantCulture()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new DerivedLanguageController().ContentNegotiation(CultureInfo.InvariantCulture));
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_ContentNegotiation_CultureInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedLanguageController().ContentNegotiation(null));
        }

        [Fact]
        public void op_LanguageNegotiation()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                const string location = "/test";

                var headers = new NameValueCollection();
                headers["Accept-Language"] = "da, en-gb;q=0.8, en;q=0.7";

                var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
                request
                    .SetupGet(x => x.Headers)
                    .Returns(headers)
                    .Verifiable();
                request
                    .SetupGet(x => x.Path)
                    .Returns(location)
                    .Verifiable();
                request
                    .SetupGet(x => x.RawUrl)
                    .Returns(location)
                    .Verifiable();

                var context = new Mock<HttpContextBase>(MockBehavior.Strict);
                context
                    .SetupGet(x => x.Request)
                    .Returns(request.Object)
                    .Verifiable();

                var controller = new DerivedLanguageController();
                controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

                var result = (SeeOtherResult)controller.LanguageNegotiation();

                Assert.Equal(location + ".en", result.Location);

                request.VerifyAll();
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_View_CultureInfoInvariantCulture_object()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DerivedLanguageController().View(CultureInfo.InvariantCulture, DateTime.UtcNow));
        }

        [Fact]
        public void op_View_CultureInfoNull_object()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                Assert.Throws<ArgumentNullException>(() => new DerivedLanguageController().View(null, DateTime.UtcNow));
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_View_CultureInfo_object()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                Assert.IsAssignableFrom<ViewResult>(new DerivedLanguageController().View(new CultureInfo("fr"), DateTime.UtcNow));
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void op_View_CultureInfo_objectNull()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;

            try
            {
                Assert.IsAssignableFrom<ViewResult>(new DerivedLanguageController().View(new CultureInfo("fr"), null));
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}