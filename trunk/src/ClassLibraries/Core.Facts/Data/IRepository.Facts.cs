namespace Cavity.Data
{
    using System.Xml.XPath;
    using Moq;
    using Xunit;

    public sealed class IRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRepository>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Delete_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");

            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Delete(urn))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Delete(urn));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Delete_AlphaDecimal()
        {
            var key = AlphaDecimal.Random();

            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Delete(key))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Delete(key));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Exists_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");

            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Exists(urn))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Exists(urn));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Exists_AlphaDecimal()
        {
            var key = AlphaDecimal.Random();

            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Exists(key))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Exists(key));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Exists_XPathExpression()
        {
            var xpath = XPathExpression.Compile("//root");
            
            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Exists(xpath))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Exists(xpath));

            mock.VerifyAll();
        }
    }
}