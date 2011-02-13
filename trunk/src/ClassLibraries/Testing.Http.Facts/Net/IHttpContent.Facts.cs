namespace Cavity.Net
{
    using System.IO;
    using System.Net.Mime;
    using Moq;
    using Xunit;

    public sealed class IHttpContentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttpContent).IsInterface);
        }

        [Fact]
        public void op_Write_Stream()
        {
            var stream = new Mock<Stream>().Object;

            var mock = new Mock<IHttpContent>();
            mock
                .Setup(x => x.Write(stream))
                .Verifiable();

            mock.Object.Write(stream);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Type_get()
        {
            var expected = new ContentType("text/plain");

            var mock = new Mock<IHttpContent>();
            mock
                .SetupGet(x => x.Type)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Type;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}