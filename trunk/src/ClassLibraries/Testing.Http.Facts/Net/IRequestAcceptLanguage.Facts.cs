namespace Cavity.Net
{
    using System.Globalization;
    using Moq;
    using Xunit;

    public sealed class IRequestAcceptLanguageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IRequestAcceptLanguage).IsInterface);
        }

        [Fact]
        public void op_AcceptAnyLanguage()
        {
            var expected = new Mock<IRequestMethod>().Object;

            var mock = new Mock<IRequestAcceptLanguage>();
            mock
                .Setup(x => x.AcceptAnyLanguage())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AcceptAnyLanguage();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_AcceptLanguage_CultureInfo()
        {
            var expected = new Mock<IRequestMethod>().Object;

            var language = new CultureInfo("en-gb");

            var mock = new Mock<IRequestAcceptLanguage>();
            mock
                .Setup(x => x.AcceptLanguage(language))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AcceptLanguage(language);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_AcceptLanguage_string()
        {
            var expected = new Mock<IRequestMethod>().Object;

            const string language = "en-gb";

            var mock = new Mock<IRequestAcceptLanguage>();
            mock
                .Setup(x => x.AcceptLanguage(language))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AcceptLanguage(language);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}