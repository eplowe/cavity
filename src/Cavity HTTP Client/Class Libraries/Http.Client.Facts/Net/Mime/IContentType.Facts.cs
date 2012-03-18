namespace Cavity.Net.Mime
{
    using System.Net.Mime;

    using Moq;

    using Xunit;

    public sealed class IContentTypeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IContentType>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_ContentType_get()
        {
            var expected = new ContentType("text/plain");

            var mock = new Mock<IContentType>();
            mock
                .SetupGet(x => x.ContentType)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ContentType;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}