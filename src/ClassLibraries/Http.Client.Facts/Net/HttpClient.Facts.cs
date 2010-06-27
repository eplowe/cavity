namespace Cavity.Net
{
    using System;
    using System.IO;
    using Cavity;
    using Cavity.Net.Mime;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpClientFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpClient>()
                .DerivesFrom<ComparableObject>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpClient>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpClient());
        }

        [Fact]
        public void ctor_IHttp()
        {
            Assert.NotNull(new HttpClient(new Http()));
        }

        [Fact]
        public void ctor_IHttpNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpClient(null as IHttp));
        }

        [Fact]
        public void prop_Response()
        {
            Assert.NotNull(new PropertyExpectations<HttpClient>("Response")
                .TypeIs<IHttpResponse>()
                .DefaultValueIsNull()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_UserAgent()
        {
            try
            {
                string value = "user agent";

                var userAgent = new Mock<IUserAgent>();
                userAgent.SetupGet<string>(x => x.Value).Returns(value).Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IUserAgent>()).Returns(userAgent.Object).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                Assert.NotNull(new PropertyExpectations<HttpClient>("UserAgent")
                    .TypeIs<string>()
                    .DefaultValueIs(value)
                    .IsNotDecorated()
                    .Result);

                userAgent.VerifyAll();
                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void op_Navigate_IHttpRequest()
        {
            try
            {
                var media = new Mock<IMediaType>();
                media
                    .Setup(x => x.ToContent(It.IsAny<TextReader>()))
                    .Returns(new Mock<IContent>().Object)
                    .Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/html")).Returns(media.Object).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                HttpRequest request =
                    "GET http://www.example.com/ HTTP/1.1" + Environment.NewLine +
                    "Host: www.example.com" + Environment.NewLine +
                    "Connection: close" + Environment.NewLine +
                    string.Empty;

                HttpClient client = new HttpClient();
                client.Navigate(request);

                Assert.Equal<string>("HTTP/1.1 200 OK", client.Response.StatusLine);
                Assert.Equal<int>(9, client.Response.Headers.Count);

                media.VerifyAll();
                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void op_Navigate_IHttpRequestNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpClient().Navigate(null as IHttpRequest));
        }

        [Fact]
        public void op_ToString()
        {
            try
            {
                string expected = "user agent";

                var userAgent = new Mock<IUserAgent>();
                userAgent.SetupGet<string>(x => x.Value).Returns(expected).Verifiable();

                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IUserAgent>()).Returns(userAgent.Object).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                string actual = new HttpClient().UserAgent;

                Assert.Equal<string>(expected, actual);

                userAgent.VerifyAll();
                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }
    }
}