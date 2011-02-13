namespace Cavity.Net
{
    using System.Net.Mime;
    using Moq;
    using Xunit;

    public sealed class IResponseContentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseContent).IsInterface);
        }

        [Fact]
        public void op_ResponseHasNoContent()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseHasNoContent())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseHasNoContent();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsApplicationJson()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsApplicationJson())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsApplicationJson();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsApplicationJson_ContentType()
        {
            var expected = new Mock<ITestHttp>().Object;

            var type = new ContentType("text/plain");

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsApplicationJson(type))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsApplicationJson(type);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsApplicationXhtml()
        {
            var expected = new Mock<IResponseHtml>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsApplicationXhtml())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsApplicationXhtml();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsApplicationXml()
        {
            var expected = new Mock<IResponseXml>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsApplicationXml())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsApplicationXml();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsApplicationXml_ContentType()
        {
            var expected = new Mock<IResponseXml>().Object;

            var type = new ContentType("application/atom+xml");

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsApplicationXml(type))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsApplicationXml(type);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsImageIcon()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsImageIcon())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsImageIcon();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsTextCss()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsTextCss())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsTextCss();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsTextHtml()
        {
            var expected = new Mock<IResponseHtml>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsTextHtml())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsTextHtml();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsTextJavaScript()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsTextJavaScript())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsTextJavaScript();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ResponseIsTextPlain()
        {
            var expected = new Mock<ITestHttp>().Object;

            var mock = new Mock<IResponseContent>();
            mock
                .Setup(x => x.ResponseIsTextPlain())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ResponseIsTextPlain();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}