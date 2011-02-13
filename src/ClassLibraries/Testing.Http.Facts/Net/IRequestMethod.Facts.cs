namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IRequestMethodFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IRequestMethod).IsInterface);
        }

        [Fact]
        public void op_Delete()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Delete())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Delete();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Get()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Get())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Get();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Head()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Head())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Head();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Options()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Options())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Options();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Post_IHttpContent()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var content = new Mock<IHttpContent>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Post(content))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Post(content);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Put_IHttpContent()
        {
            var expected = new Mock<IResponseStatus>().Object;

            var content = new Mock<IHttpContent>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Put(content))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Put(content);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Use_string()
        {
            var expected = new Mock<IResponseStatus>().Object;

            const string method = "EXAMPLE";

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Use(method))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Use(method);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Use_string_IHttpContent()
        {
            var expected = new Mock<IResponseStatus>().Object;

            const string method = "EXAMPLE";
            var content = new Mock<IHttpContent>().Object;

            var mock = new Mock<IRequestMethod>();
            mock
                .Setup(x => x.Use(method, content))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Use(method, content);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}