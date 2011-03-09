namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Moq;
    using Xunit;

    public sealed class IRepositoryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRepository<int>>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Delete_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");

            var mock = new Mock<IRepository<int>>();
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

            var mock = new Mock<IRepository<int>>();
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

            var mock = new Mock<IRepository<int>>();
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

            var mock = new Mock<IRepository<int>>();
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

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Exists(xpath))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Exists(xpath));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Insert_IRecord()
        {
            var expected = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Insert(expected))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Insert(expected);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Key_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            var expected = AlphaDecimal.Random();

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Key(urn))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Key(urn);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Match_AbsoluteUri_string()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            const string etag = "\"xyz\"";

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Match(urn, etag))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Match(urn, etag));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Match_AlphaDecimal_string()
        {
            var key = AlphaDecimal.Random();
            const string etag = "\"xyz\"";

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Match(key, etag))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Match(key, etag));

            mock.VerifyAll();
        }

        [Fact]
        public void op_ModifiedSince_AbsoluteUri_string()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            const string etag = "\"xyz\"";

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ModifiedSince(urn, etag))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.ModifiedSince(urn, etag));

            mock.VerifyAll();
        }

        [Fact]
        public void op_ModifiedSince_AlphaDecimal_string()
        {
            var key = AlphaDecimal.Random();
            const string etag = "\"xyz\"";

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ModifiedSince(key, etag))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.ModifiedSince(key, etag));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Query_XPathExpression()
        {
            var xpath = XPathExpression.Compile("//root");
            var expected = new List<IRecord<int>>
            {
                new Mock<IRecord<int>>().Object
            };

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Query(xpath))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Query(xpath);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Select_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            var expected = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Select(urn))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Select(urn);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Select_AlphaDecimal()
        {
            var key = AlphaDecimal.Random();
            var expected = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Select(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Select(key);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Update_IRecord()
        {
            var record = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Update(record))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Update(record));

            mock.VerifyAll();
        }

        [Fact]
        public void op_Upsert_IRecord()
        {
            var record = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Upsert(record))
                .Verifiable();

            mock.Object.Upsert(record);

            mock.VerifyAll();
        }
    }
}