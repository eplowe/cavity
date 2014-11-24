namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml;
    using Cavity.Xml.XPath;
    using Moq;
    using Xunit;

    public sealed class RedirectionResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RedirectionResult>()
                            .DerivesFrom<ActionResult>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DerivedRedirectionResult());
        }

        [Fact]
        public void ctor_string()
        {
            const string expected = "/";
            var actual = new DerivedRedirectionResult().Location;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DerivedRedirectionResult(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedRedirectionResult(null));
        }

        [Fact]
        public void op_Content_DateTime_AbsoluteUri()
        {
            var date = DateTime.UtcNow;

            var xml = new XmlDocument();
            xml.LoadXml(RedirectionResult.Content(date, "http://example.com/"));

            var navigator = xml.CreateNavigator();

            Assert.True(navigator.Evaluate<bool>("1 = count(/html/head/meta[@value='{0}'])".FormatWith(date.ToXmlString())));
            Assert.True(navigator.Evaluate<bool>("1 = count(//a[@id='location'][@href='{0}'])".FormatWith("http://example.com/")));
            Assert.True(navigator.Evaluate<bool>("1 = count(//a[@id='location'][text()='{0}'])".FormatWith("http://example.com/")));
        }

        [Fact]
        public void op_Content_DateTime_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => RedirectionResult.Content(DateTime.UtcNow, null));
        }

        [Fact]
        public void op_ExecuteResult_ControllerContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedRedirectionResult().ExecuteResult(null));
        }

        [Fact]
        public void op_ExecuteResult_FilterExecutingContext()
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request
                .Setup(x => x.Url)
                .Returns(new Uri("http://example.com"));

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.Ambiguous)
                .Verifiable();
            response
                .Setup(x => x.AppendHeader("Location", "http://example.com/"))
                .Verifiable();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.Server))
                .Verifiable();
            response
                .Setup(x => x.Cache.SetExpires(It.IsAny<DateTime>()))
                .Verifiable();
            response
                .SetupSet(x => x.ContentType = "text/html")
                .Verifiable();
            response
                .Setup(x => x.Write(It.IsAny<string>()))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .Setup(x => x.Request)
                .Returns(request.Object);
            context
                .Setup(x => x.Response)
                .Returns(response.Object);

            new DerivedRedirectionResult(HttpStatusCode.Ambiguous).ExecuteResult(new ControllerContext
                                                                                     {
                                                                                         HttpContext = context.Object
                                                                                     });

            response.VerifyAll();
        }

        [Fact]
        public void prop_Location()
        {
            Assert.NotNull(new PropertyExpectations<RedirectionResult>(x => x.Location)
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_StatusCode()
        {
            Assert.NotNull(new PropertyExpectations<RedirectionResult>(x => x.StatusCode)
                               .TypeIs<HttpStatusCode>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}