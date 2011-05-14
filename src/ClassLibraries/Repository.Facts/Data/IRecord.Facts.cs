namespace Cavity.Data
{
    using System;
    using Cavity.Net;
    using Moq;
    using Xunit;

    public sealed class IRecordFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRecord>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_ToEntity()
        {
            const string expected = "123";

            var mock = new Mock<IRecord>();
            mock
                .Setup(x => x.ToEntity())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToEntity();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ToXml()
        {
            var expected = 123.XmlSerialize();

            var mock = new Mock<IRecord>();
            mock
                .Setup(x => x.ToXml())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToXml();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Cacheability_get()
        {
            const string expected = "public";

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Cacheability)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Cacheability;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Cacheability_set()
        {
            const string value = "public";

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Cacheability = value)
                .Verifiable();

            mock.Object.Cacheability = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Created_get()
        {
            DateTime? expected = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Created)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Created;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Created_set()
        {
            DateTime? value = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Created = value)
                .Verifiable();

            mock.Object.Created = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Etag_get()
        {
            EntityTag expected = "\"xyz\"";

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Etag)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Etag;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Etag_set()
        {
            EntityTag value = "\"xyz\"";

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Etag = value)
                .Verifiable();

            mock.Object.Etag = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Expiration_get()
        {
            const string expected = "P1D";

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Expiration)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Expiration;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Expiration_set()
        {
            const string value = "P1D";

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Expiration = value)
                .Verifiable();

            mock.Object.Expiration = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Key_get()
        {
            AlphaDecimal? expected = AlphaDecimal.Random();

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Key)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Key;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Key_set()
        {
            AlphaDecimal? value = AlphaDecimal.Random();

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Key = value)
                .Verifiable();

            mock.Object.Key = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Modified_get()
        {
            DateTime? expected = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Modified)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Modified;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Modified_set()
        {
            DateTime? value = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Modified = value)
                .Verifiable();

            mock.Object.Modified = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Status_get()
        {
            int? expected = 200;

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Status)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Status;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Status_set()
        {
            int? value = 200;

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Status = value)
                .Verifiable();

            mock.Object.Status = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Urn_get()
        {
            AbsoluteUri expected = "urn://example.com/path";

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Urn)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Urn;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Urn_set()
        {
            AbsoluteUri value = "urn://example.com/path";

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Urn = value)
                .Verifiable();

            mock.Object.Urn = value;

            mock.VerifyAll();
        }
    }
}