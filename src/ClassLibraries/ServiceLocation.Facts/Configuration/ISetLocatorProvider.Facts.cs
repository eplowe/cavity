namespace Cavity.Configuration
{
    using Moq;
    using Xunit;

    public sealed class ISetLocatorProviderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ISetLocatorProvider).IsInterface);
        }

        [Fact]
        public void op_Configure()
        {
            var mock = new Mock<ISetLocatorProvider>();
            mock
                .Setup(x => x.Configure())
                .Verifiable();

            mock.Object.Configure();

            mock.VerifyAll();
        }
    }
}