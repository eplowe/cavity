namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.XPath;
    using Cavity.Data;
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
        public void op_Load_FileSystemInfo()
        {
            var key = AlphaDecimal.Random();
            var expected = new Record<int>
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

            using (var file = new TempFile())
            {
                using (var stream = file.Info.Open(FileMode.Truncate, FileAccess.Write, FileShare.Read))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("urn: urn://example.com/abc");
                        writer.WriteLine("key: " + key);
                        writer.WriteLine("etag: \"xyz\"");
                        writer.WriteLine("created: 1999-12-31T01:00:00Z");
                        writer.WriteLine("modified: 2001-12-31T01:00:00Z");
                        writer.WriteLine("cacheability: public");
                        writer.WriteLine("expiration: P1D");
                        writer.WriteLine("status: 200");
                        writer.WriteLine(string.Empty);
                        writer.Write("<int>123</int>");
                    }
                }

                var actual = RecordFile.Load(file.Info).ToRecord<int>();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Load_FileSystemInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => RecordFile.Load(null));
        }

        [Fact]
        public void op_Save_FileSystemInfo()
        {
            var record = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = AlphaDecimal.Random(),
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = 123
            };

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("key: " + record.Key);
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine("created: 1999-12-31T01:00:00Z");
            expected.AppendLine("modified: 2001-12-31T01:00:00Z");
            expected.AppendLine("cacheability: public");
            expected.AppendLine("expiration: P1D");
            expected.AppendLine("status: 200");
            expected.AppendLine(string.Empty);
            expected.Append("<int>123</int>");

            string actual;
            using (var root = new TempDirectory())
            {
                var obj = new RecordFile(record);
                obj.Save(root.Info);

                actual = new FileInfo(obj.Location.FullName).ReadToEnd();
            }

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_Save_FileSystemInfoNull()
        {
            var record = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = AlphaDecimal.Random(),
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = 123
            };

            Assert.Throws<ArgumentNullException>(() => new RecordFile(record).Save(null));
        }

        [Fact]
        public void op_Save_FileSystemInfo_whenNullKey()
        {
            var record = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = null,
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = 123
            };

            var root = new DirectoryInfo(Path.GetTempPath());

            Assert.Throws<InvalidOperationException>(() => new RecordFile(record).Save(root));
        }

        [Fact]
        public void op_Save_FileSystemInfo_whenNullUrn()
        {
            var record = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = AlphaDecimal.Random(),
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = null,
                Value = 123
            };

            var root = new DirectoryInfo(Path.GetTempPath());

            Assert.Throws<InvalidOperationException>(() => new RecordFile(record).Save(root));
        }

        [Fact]
        public void op_ToRecordOfT()
        {
            var expected = new Record<int>
            {
                Cacheability = "public",
                Created = new DateTime(1999, 12, 31, 01, 00, 00, 00),
                Etag = "\"xyz\"",
                Expiration = "P1D",
                Key = AlphaDecimal.Random(),
                Modified = new DateTime(2001, 12, 31, 01, 00, 00, 00),
                Status = 200,
                Urn = "urn://example.com/abc",
                Value = 123
            };

            var obj = new RecordFile(expected);

            var actual = obj.ToRecord<int>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new RecordFile
            {
                Body = "<root />"
            };
            obj.Headers["urn"] = "urn://example.com/abc";
            obj.Headers["etag"] = "\"xyz\"";

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("key: ");
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine("created: ");
            expected.AppendLine("modified: ");
            expected.AppendLine("cacheability: ");
            expected.AppendLine("expiration: ");
            expected.AppendLine("status: ");
            expected.AppendLine(string.Empty);
            expected.Append("<root />");

            var actual = obj.ToString();

            Assert.Equal(expected.ToString(), actual);
        }

        [Fact]
        public void op_ToString_whenNullBody()
        {
            var obj = new RecordFile();
            obj.Headers["urn"] = "urn://example.com/abc";
            obj.Headers["etag"] = "\"xyz\"";

            var expected = new StringBuilder();
            expected.AppendLine("urn: urn://example.com/abc");
            expected.AppendLine("key: ");
            expected.AppendLine("etag: \"xyz\"");
            expected.AppendLine("created: ");
            expected.AppendLine("modified: ");
            expected.AppendLine("cacheability: ");
            expected.AppendLine("expiration: ");
            expected.AppendLine("status: ");
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
        public void prop_Location()
        {
            Assert.True(new PropertyExpectations<RecordFile>(x => x.Location)
                            .TypeIs<FileSystemInfo>()
                            .DefaultValueIsNull()
                            .Result);
        }
    }
}