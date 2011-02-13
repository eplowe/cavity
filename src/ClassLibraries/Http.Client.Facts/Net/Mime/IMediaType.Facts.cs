namespace Cavity.Net.Mime
{
    using System.IO;
    using Moq;
    using Xunit;

    public sealed class IMediaTypeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IMediaType).IsInterface);
        }

        [Fact]
        public void op_ToContent_TextReader()
        {
            var expected = new Mock<IContent>().Object;

            var reader = new Mock<TextReader>().Object;

            var mock = new Mock<IMediaType>();
            mock
                .Setup(x => x.ToContent(reader))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToContent(reader);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}