namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class ITestHttpFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITestHttp>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_HasContentLocation_AbsoluteUri()
        {
            var expected = new Mock<ITestHttp>().Object;

            AbsoluteUri location = "http://example.com/";

            var mock = new Mock<ITestHttp>();
            mock
                .Setup(x => x.HasContentLocation(location))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasContentLocation(location);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Location()
        {
            const string expected = "Example";

            var mock = new Mock<ITestHttp>();
            mock
                .SetupGet(x => x.Location)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Location;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Result()
        {
            const bool expected = true;

            var mock = new Mock<ITestHttp>();
            mock
                .SetupGet(x => x.Result)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Result;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}