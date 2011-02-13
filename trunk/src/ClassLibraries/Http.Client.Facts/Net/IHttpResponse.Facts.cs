namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IHttpResponseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttpResponse).IsInterface);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpResponse).Implements(typeof(IHttpMessage)));
        }

        [Fact]
        public void prop_StatusLine_get()
        {
            StatusLine expected = "HTTP/1.1 200 OK";

            var mock = new Mock<IHttpResponse>();
            mock
                .SetupGet(x => x.StatusLine)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.StatusLine;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}