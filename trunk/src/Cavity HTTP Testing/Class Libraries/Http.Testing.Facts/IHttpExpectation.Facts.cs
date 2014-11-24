namespace Cavity
{
    using System.Net;
    using Moq;
    using Xunit;

    public sealed class IHttpExpectationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpExpectation>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Verify_CookieContainer()
        {
            var cookies = new CookieContainer();

            var mock = new Mock<IHttpExpectation>();
            mock
                .Setup(x => x.Verify(cookies))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Verify(cookies));

            mock.VerifyAll();
        }
    }
}