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

        ////[Fact]
        ////public void ctor_Record()
        ////{
        ////    var record = new Record<int>
        ////    {
        ////        Cacheability = "public",
        ////        Created = DateTime.UtcNow,
        ////        Etag = "123",
        ////        Expiration = "P1D",
        ////        Key = AlphaDecimal.Random(),
        ////        Modified = DateTime.UtcNow,
        ////        Status = 200,
        ////        Urn = "urn://example.com/abc",
        ////        Value = 123
        ////    };

        ////    var obj = new RecordFile(record);

        ////    Assert.NotNull(new RecordFile(record));
        ////}

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
            var xml = new XmlDocument();
            xml.LoadXml("<root />");

            var obj = new RecordFile
            {
                Body = xml
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
    }
}