namespace Cavity.Net
{
    using Cavity;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpClientSettingsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpClientSettings>()
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

                Assert.NotNull(new HttpClientSettings());

                mock.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void prop_KeepAlive()
        {
            try
            {
                var value = new UserAgent();

                var mock = new Mock<IServiceLocator>();
                mock.Setup(e => e.GetInstance<IUserAgent>()).Returns(value).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mock.Object));

                Assert.True(new PropertyExpectations<HttpClientSettings>("KeepAlive")
                    .IsAutoProperty<bool>()
                    .IsNotDecorated()
                    .Result);

                mock.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void prop_UserAgent()
        {
            try
            {
                var value = new UserAgent();

                var mock = new Mock<IServiceLocator>();
                mock.Setup(e => e.GetInstance<IUserAgent>()).Returns(value).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mock.Object));

                Assert.True(new PropertyExpectations<HttpClientSettings>("UserAgent")
                    .TypeIs<IUserAgent>()
                    .DefaultValueIs(value)
                    .ArgumentNullException()
                    .Set(new UserAgent())
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