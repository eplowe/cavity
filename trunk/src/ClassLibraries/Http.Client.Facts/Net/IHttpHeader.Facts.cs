namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IHttpHeaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpHeader>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_Name_get()
        {
            Token expected = "Example";

            var mock = new Mock<IHttpHeader>();
            mock
                .SetupGet(x => x.Name)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Name;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Value_get()
        {
            const string expected = "Example";

            var mock = new Mock<IHttpHeader>();
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