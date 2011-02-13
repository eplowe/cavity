namespace Cavity.Net
{
    using System.Globalization;
    using Moq;
    using Xunit;

    public sealed class IResponseContentLanguageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseContentLanguage).IsInterface);
        }

        [Fact]
        public void op_HasContentLanguage_CultureInfo()
        {
            var expected = new Mock<IResponseContentMD5>().Object;

            var language = new CultureInfo("en-gb");

            var mock = new Mock<IResponseContentLanguage>();
            mock
                .Setup(x => x.HasContentLanguage(language))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasContentLanguage(language);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasContentLanguage_string()
        {
            var expected = new Mock<IResponseContentMD5>().Object;

            const string language = "en-gb";

            var mock = new Mock<IResponseContentLanguage>();
            mock
                .Setup(x => x.HasContentLanguage(language))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasContentLanguage(language);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IgnoreContentLanguage()
        {
            var expected = new Mock<IResponseContentMD5>().Object;

            var mock = new Mock<IResponseContentLanguage>();
            mock
                .Setup(x => x.IgnoreContentLanguage())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IgnoreContentLanguage();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}