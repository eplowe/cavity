namespace Cavity.Net
{
    using Moq;

    using Xunit;

    public sealed class IUserAgentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IUserAgent>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_Value_get()
        {
            const string expected = "Example";

            var mock = new Mock<IUserAgent>();
            mock
                .SetupGet(x => x.Value)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Value;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}