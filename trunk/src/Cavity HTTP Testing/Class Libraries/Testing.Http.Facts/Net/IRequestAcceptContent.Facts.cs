namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IRequestAcceptContentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRequestAcceptContent>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_AcceptAnyContent()
        {
            var expected = new Mock<IRequestAcceptLanguage>().Object;

            var mock = new Mock<IRequestAcceptContent>();
            mock
                .Setup(x => x.AcceptAnyContent())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AcceptAnyContent();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Accept_string()
        {
            var expected = new Mock<IRequestAcceptLanguage>().Object;

            const string value = "text/plain; text/html";

            var mock = new Mock<IRequestAcceptContent>();
            mock
                .Setup(x => x.Accept(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Accept(value);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}