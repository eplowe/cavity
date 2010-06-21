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
                .DerivesFrom<ValueObject<HttpClient>>()
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
                var mock = new Mock<IServiceLocator>();
                mock.Setup(e => e.GetInstance<IUserAgent>()).Returns(new UserAgent()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => mock.Object));

                Assert.NotNull(new PropertyExpectations<HttpClient>("UserAgent")
                    .TypeIs<string>()
                    .DefaultValueIs(UserAgent.Format(1, 0))
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