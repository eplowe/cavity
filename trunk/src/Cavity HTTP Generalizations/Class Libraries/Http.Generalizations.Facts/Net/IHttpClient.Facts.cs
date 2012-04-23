namespace Cavity.Net
{
    using System;
    using System.Net;

    using Cavity;

    using Moq;

    using Xunit;

    public sealed class IHttpClientFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpClient>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void prop_AutoRedirect_get()
        {
            const bool expected = true;
            var mock = new Mock<IHttpClient>();
            mock
                .Setup(x => x.AutoRedirect)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AutoRedirect;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_AutoRedirect_set()
        {
            var mock = new Mock<IHttpClient>();
            mock
                .SetupSet(x => x.AutoRedirect = false)
                .Verifiable();

            mock.Object.AutoRedirect = false;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_CookieContainer_get()
        {
            var expected = new CookieContainer();
            var mock = new Mock<IHttpClient>();
            mock
                .Setup(x => x.Cookies)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Cookies;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}