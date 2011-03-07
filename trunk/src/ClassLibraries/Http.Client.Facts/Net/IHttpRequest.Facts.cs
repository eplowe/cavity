namespace Cavity.Net
{
    using System;
    using Moq;
    using Xunit;

    public sealed class IHttpRequestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpRequest>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpRequest).Implements(typeof(IHttpMessage)));
        }

        [Fact]
        public void prop_AbsoluteUri_get()
        {
            var expected = new Uri("http://example.com/");

            var mock = new Mock<IHttpRequest>();
            mock
                .SetupGet(x => x.AbsoluteUri)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AbsoluteUri;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_RequestLine_get()
        {
            RequestLine expected = "GET / HTTP/1.1";

            var mock = new Mock<IHttpRequest>();
            mock
                .SetupGet(x => x.RequestLine)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.RequestLine;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}