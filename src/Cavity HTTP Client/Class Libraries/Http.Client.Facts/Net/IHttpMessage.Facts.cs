namespace Cavity.Net
{
    using System.IO;
    using Cavity.Net.Mime;
    using Moq;
    using Xunit;

    public sealed class IHttpMessageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpMessage>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Read_TextReader()
        {
            var reader = new Mock<TextReader>().Object;

            var mock = new Mock<IHttpMessage>();
            mock
                .Setup(x => x.Read(reader))
                .Verifiable();

            mock.Object.Read(reader);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Write_TextWriter()
        {
            var writer = new Mock<TextWriter>().Object;

            var mock = new Mock<IHttpMessage>();
            mock
                .Setup(x => x.Write(writer))
                .Verifiable();

            mock.Object.Write(writer);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Body_get()
        {
            var expected = new Mock<IContent>().Object;

            var mock = new Mock<IHttpMessage>();
            mock
                .SetupGet(x => x.Body)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Body;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Headers_get()
        {
            var expected = new HttpHeaderCollection();

            var mock = new Mock<IHttpMessage>();
            mock
                .SetupGet(x => x.Headers)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Headers;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}