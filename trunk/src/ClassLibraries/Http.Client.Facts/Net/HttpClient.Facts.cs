namespace Cavity.Net
{
    using Cavity;
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