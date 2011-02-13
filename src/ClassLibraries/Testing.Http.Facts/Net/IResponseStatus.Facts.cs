namespace Cavity.Net
{
    using System.Net;
    using Moq;
    using Xunit;

    public sealed class IResponseStatusFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseStatus).IsInterface);
        }

        [Fact]
        public void op_IsSeeOther_AbsoluteUri()
        {
            var expected = new Mock<ITestHttp>().Object;

            AbsoluteUri location = "http://example.com/";

            var mock = new Mock<IResponseStatus>();
            mock
                .Setup(x => x.IsSeeOther(location))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsSeeOther(location);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Is_HttpStatusCode()
        {
            var expected = new Mock<IResponseCacheControl>().Object;

            const HttpStatusCode status = 0;

            var mock = new Mock<IResponseStatus>();
            mock
                .Setup(x => x.Is(status))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Is(status);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}