namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IHttpFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttp).IsInterface);
        }

        [Fact]
        public void op_Send_IHttpRequest()
        {
            var expected = new Mock<IHttpResponse>().Object;

            var request = new Mock<IHttpRequest>().Object;

            var mock = new Mock<IHttp>();
            mock
                .Setup(x => x.Send(request))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Send(request);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}