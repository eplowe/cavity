namespace Cavity.Net.Mime
{
    using System.IO;

    using Moq;

    using Xunit;

    public sealed class IContentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IContent>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void is_IContentType()
        {
            Assert.True(typeof(IContent).Implements(typeof(IContentType)));
        }

        [Fact]
        public void op_Write_TextWriter()
        {
            var writer = new Mock<TextWriter>().Object;

            var mock = new Mock<IContent>();
            mock
                .Setup(x => x.Write(writer))
                .Verifiable();

            mock.Object.Write(writer);

            mock.VerifyAll();
        }
    }
}