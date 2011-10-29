namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IResponseContentMD5Facts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IResponseContentMD5>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_HasContentMD5()
        {
            var expected = new Mock<IResponseContent>().Object;

            var mock = new Mock<IResponseContentMD5>();
            mock
                .Setup(x => x.HasContentMD5())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasContentMD5();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IgnoreContentMD5()
        {
            var expected = new Mock<IResponseContent>().Object;

            var mock = new Mock<IResponseContentMD5>();
            mock
                .Setup(x => x.IgnoreContentMD5())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IgnoreContentMD5();

            Assert.Same(expected, actual);
        }
    }
}