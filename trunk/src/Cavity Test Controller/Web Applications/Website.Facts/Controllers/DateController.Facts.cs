namespace Cavity.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Cavity.Globalization;
    using Cavity.Web.Mvc;
    using Cavity.Web.Routing;

    using Xunit;

    public sealed class DateControllerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DateController>()
                            .DerivesFrom<LanguageController>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsDecoratedWith<AllowAttribute>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DateController());
        }

        [Fact]
        public void op_HtmlRepresentation_Language_DateTime()
        {
            Assert.IsType<ViewResult>(new DateController().HtmlRepresentation(new CultureInfo("en"), DateTime.Today));
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollection()
        {
            var routes = new RouteCollection();

            var controller = (IRegisterRoutes)new DateController();
            controller.Register(routes);

            Assert.Equal("today", ((Route)routes["Today (lanneg)"]).Url);
            Assert.Equal("today.{language}", ((Route)routes["Today (conneg)"]).Url);
            Assert.Equal("today.{language}.html", ((Route)routes["Today"]).Url);
        }

        [Fact]
        public void op_RegisterRoutes_RouteCollectionNull()
        {
            var controller = (IRegisterRoutes)new DateController();

            Assert.Throws<ArgumentNullException>(() => controller.Register(null));
        }

        [Fact]
        public void prop_Languages()
        {
            foreach (var language in new DateController().Languages)
            {
                Assert.Equal<Language>("en", language);
            }
        }
    }
}