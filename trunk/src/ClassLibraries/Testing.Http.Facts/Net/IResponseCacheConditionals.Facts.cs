namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IResponseCacheConditionalsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseCacheConditionals).IsInterface);
        }

        [Fact]
        public void op_IgnoreCacheConditionals()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheConditionals>();
            mock
                .Setup(x => x.IgnoreCacheConditionals())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IgnoreCacheConditionals();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_WithEtag()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheConditionals>();
            mock
                .Setup(x => x.WithEtag())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.WithEtag();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_WithExpires()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheConditionals>();
            mock
                .Setup(x => x.WithExpires())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.WithExpires();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_WithLastModified()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheConditionals>();
            mock
                .Setup(x => x.WithLastModified())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.WithLastModified();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}