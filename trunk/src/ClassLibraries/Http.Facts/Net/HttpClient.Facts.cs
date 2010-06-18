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
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            try
            {
                var mock = new Mock<IServiceLocator>();
                mock.Setup(e => e.GetInstance<IUserAgent>()).Returns(new UserAgent()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mock.Object));

                Assert.NotNull(new HttpClient());

                mock.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void prop_Settings()
        {
            try
            {
                var mock = new Mock<IServiceLocator>();
                mock.Setup(e => e.GetInstance<IUserAgent>()).Returns(new UserAgent()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mock.Object));

                Assert.NotNull(new PropertyExpectations<HttpClient>("Settings")
                    .TypeIs<HttpClientSettings>()
                    .DefaultValueIsNotNull()
                    .ArgumentNullException()
                    .IsNotDecorated()
                    .Result);

                mock.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }
    }
}