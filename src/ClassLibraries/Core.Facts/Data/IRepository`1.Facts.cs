namespace Cavity.Data
{
    using System;
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
        public void op_ModifiedSince_AbsoluteUri_DateTime()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            var value = DateTime.UtcNow;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ModifiedSince(urn, value))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.ModifiedSince(urn, value));

            mock.VerifyAll();
        }

        [Fact]
        public void op_ModifiedSince_AlphaDecimal_DateTime()
        {
            var key = AlphaDecimal.Random();
            var value = DateTime.UtcNow;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ModifiedSince(key, value))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.ModifiedSince(key, value));

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
        public void op_ToKey_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");
            var expected = AlphaDecimal.Random();

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ToKey(urn))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToKey(urn);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ToUrn_AlphaDecimal()
        {
            var key = AlphaDecimal.Random();
            var expected = new AbsoluteUri("urn://example.com/path");

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.ToUrn(key))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToUrn(key);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Update_IRecord()
        {
            var expected = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Update(expected))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Update(expected);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Upsert_IRecord()
        {
            var expected = new Mock<IRecord<int>>().Object;

            var mock = new Mock<IRepository<int>>();
            mock
                .Setup(x => x.Upsert(expected))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Upsert(expected);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}