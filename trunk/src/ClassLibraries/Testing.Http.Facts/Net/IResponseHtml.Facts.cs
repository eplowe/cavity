namespace Cavity.Net
{
    using Moq;
    using Xunit;

    public sealed class IResponseHtmlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IResponseHtml>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void is_ITestHttp()
        {
            Assert.True(typeof(IResponseHtml).Implements(typeof(ITestHttp)));
        }

        [Fact]
        public void op_EvaluateFalse_string()
        {
            var expected = new Mock<IResponseHtml>().Object;

            var mock = new Mock<IResponseHtml>();
            mock
                .Setup(x => x.EvaluateFalse("/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EvaluateFalse("/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_EvaluateTrue_string()
        {
            var expected = new Mock<IResponseHtml>().Object;

            var mock = new Mock<IResponseHtml>();
            mock
                .Setup(x => x.EvaluateTrue("/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EvaluateTrue("/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Evaluate_T_string()
        {
            var expected = new Mock<IResponseHtml>().Object;

            var mock = new Mock<IResponseHtml>();
            mock
                .Setup(x => x.Evaluate(double.Epsilon, "/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Evaluate(double.Epsilon, "/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasRobotsTag_string()
        {
            var expected = new Mock<IResponseHtml>().Object;

            const string value = "Example";

            var mock = new Mock<IResponseHtml>();
            mock
                .Setup(x => x.HasRobotsTag(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasRobotsTag(value);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_HasStyleSheetLink_string()
        {
            var expected = new Mock<IResponseHtml>().Object;

            const string href = "/site.css";

            var mock = new Mock<IResponseHtml>();
            mock
                .Setup(x => x.HasStyleSheetLink(href))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasStyleSheetLink(href);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}