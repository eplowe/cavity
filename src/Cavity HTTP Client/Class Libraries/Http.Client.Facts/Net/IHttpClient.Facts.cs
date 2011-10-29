namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IHttpClientFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpClient>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_UserAgent_get()
        {
            const string expected = "Example";

            var mock = new Mock<IHttpClient>();
            mock
                .SetupGet(x => x.UserAgent)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.UserAgent;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}