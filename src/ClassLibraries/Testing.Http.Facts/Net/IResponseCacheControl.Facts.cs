namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IResponseCacheControlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseCacheControl).IsInterface);
        }

        [Fact]
        public void op_HasCacheControlNone()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheControl>();
            mock
                .Setup(x => x.HasCacheControlNone())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasCacheControlNone();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasCacheControlPrivate()
        {
            var expected = new Mock<IResponseCacheConditionals>().Object;

            var mock = new Mock<IResponseCacheControl>();
            mock
                .Setup(x => x.HasCacheControlPrivate())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasCacheControlPrivate();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasCacheControlPublic()
        {
            var expected = new Mock<IResponseCacheConditionals>().Object;

            var mock = new Mock<IResponseCacheControl>();
            mock
                .Setup(x => x.HasCacheControlPublic())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasCacheControlPublic();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasCacheControl_string()
        {
            var expected = new Mock<IResponseCacheConditionals>().Object;

            const string value = "public";

            var mock = new Mock<IResponseCacheControl>();
            mock
                .Setup(x => x.HasCacheControl(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasCacheControl(value);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IgnoreCacheControl()
        {
            var expected = new Mock<IResponseContentLanguage>().Object;

            var mock = new Mock<IResponseCacheControl>();
            mock
                .Setup(x => x.IgnoreCacheControl())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IgnoreCacheControl();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}