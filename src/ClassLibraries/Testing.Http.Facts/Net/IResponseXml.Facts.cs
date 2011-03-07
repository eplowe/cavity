namespace Cavity.Net
{
    using Cavity.Xml;
    using Moq;
    using Xunit;

    public sealed class IResponseXmlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IResponseXml>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void is_ITestHttp()
        {
            Assert.True(typeof(IResponseXml).Implements(typeof(ITestHttp)));
        }

        [Fact]
        public void op_EvaluateFalse()
        {
            var expected = new Mock<IResponseXml>().Object;

            var mock = new Mock<IResponseXml>();
            mock
                .Setup(x => x.EvaluateFalse("/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EvaluateFalse("/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_EvaluateTrue()
        {
            var expected = new Mock<IResponseXml>().Object;

            var mock = new Mock<IResponseXml>();
            mock
                .Setup(x => x.EvaluateTrue("/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EvaluateTrue("/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Evaluate_T_string_XmlNamespaces()
        {
            var expected = new Mock<IResponseXml>().Object;

            XmlNamespace[] namespaces = null;

            var mock = new Mock<IResponseXml>();
            mock
                .Setup(x => x.Evaluate(double.Epsilon, "/x", namespaces))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Evaluate(double.Epsilon, "/x", namespaces);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Evaluate_T_strings()
        {
            var expected = new Mock<IResponseXml>().Object;

            var mock = new Mock<IResponseXml>();
            mock
                .Setup(x => x.Evaluate(double.Epsilon, "/x", "/y", "/z"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Evaluate(double.Epsilon, "/x", "/y", "/z");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}