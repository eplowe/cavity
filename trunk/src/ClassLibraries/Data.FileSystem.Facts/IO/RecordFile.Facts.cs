namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity;
    using Cavity.Data;
    using Cavity.Net;
    using Xunit;

    public sealed class RecordFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RecordFile>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RecordFile());
        }

        [Fact]
        public void ctor_IRecord()
        {
            Assert.NotNull(new RecordFile(new Record<int>()));
        }

        [Fact]
        public void ctor_IRecordNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RecordFile(null));
        }

        [Fact]
        public void prop_Body()
        {
            Assert.True(new PropertyExpectations<RecordFile>(x => x.Body)
                .IsAutoProperty<IXPathNavigable>()
                .Result);
        }

        [Fact]
        public void prop_Header()
        {
            Assert.True(new PropertyExpectations<RecordFile>(x => x.Headers)
                .TypeIs<IDictionary<string, string>>()
                .DefaultValueIsNotNull()
                .Result);
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new RecordFile
            {
                Body = "<root />"
            };
            obj.Headers.Add("urn", "urn://example.com/abc");
            obj.Headers.Add("etag", "\"xyz\"");

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine(string.Empty);
            expected.Append("<root />");

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenNullBody()
        {
            var obj = new RecordFile();
            obj.Headers.Add("urn", "urn://example.com/abc");
            obj.Headers.Add("etag", "\"xyz\"");

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine(string.Empty);

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenRecord()
        {
            var key = AlphaDecimal.Random();
            var record = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = key,
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = 123
            };

            var obj = new RecordFile(record);

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("key: " + key);
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine("created: 1999-12-31T01:00:00Z");
            expected.AppendLine("modified: 2001-12-31T01:00:00Z");
            expected.AppendLine("cacheability: public");
            expected.AppendLine("expiration: P1D");
            expected.AppendLine("status: 200");
            expected.AppendLine(string.Empty);
            expected.Append("<int>123</int>");

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenRecordNullXml()
        {
            var key = AlphaDecimal.Random();
            var record = new Record<string>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = key,
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = null
            };

            var obj = new RecordFile(record);

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("key: " + key);
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine("created: 1999-12-31T01:00:00Z");
            expected.AppendLine("modified: 2001-12-31T01:00:00Z");
            expected.AppendLine("cacheability: public");
            expected.AppendLine("expiration: P1D");
            expected.AppendLine("status: 200");
            expected.AppendLine(string.Empty);

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToXml()
        {
            const string expected = "<root />";

            var obj = new RecordFile
            {
                Body = expected
            };

            var actual = obj.ToXml().CreateNavigator().OuterXml;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToXml_whenNullBody()
        {
            Assert.Null(new RecordFile().ToXml());
        }
    }
}