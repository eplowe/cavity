namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Collections;
    using Cavity.Data;
    using Xunit;

    public sealed class CsvFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CsvFile>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<KeyStringDictionary>>()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            Assert.NotNull(new CsvFile(new FileInfo("test.csv")));
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvFile(null as FileInfo));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new CsvFile("test.csv"));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");

                foreach (var item in new CsvFile(file.Info))
                {
                    Assert.Equal("value", item["name"]);
                }
            }
        }

        [Fact]
        public void op_GetEnumerator_whenOnlyHeader()
        {
            using (var file = new TempFile())
            {
                file.Info.Append("name");
                foreach (var item in new CsvFile(file.Info))
                {
                    Assert.Equal("value", item["name"]);
                }
            }
        }

        [Fact]
        public void op_Header_KeyStringDictionary()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("A,B", string.Empty),
                new KeyStringPair("C", string.Empty)
            };

            const string expected = "\"A,B\",C";
            var actual = CsvFile.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Header_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair(string.Empty, "x"),
                new KeyStringPair("A,B", "x")
            };

            const string expected = ",\"A,B\"";
            var actual = CsvFile.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Header_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CsvFile.Header(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Header_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Header(null));
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            using (var file = new TempFile())
            {
                file.Info.Append("name");
                file.Info.Append("value");
                IEnumerable enumerable = new CsvFile(file.Info);
                foreach (var entry in enumerable.Cast<KeyStringDictionary>())
                {
                    Assert.Equal("value", entry["name"]);
                }
            }
        }

        [Fact]
        public void op_Line_KeyStringDictionary()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("A", "123"),
                new KeyStringPair("B", "left,right")
            };

            const string expected = "123,\"left,right\"";
            var actual = CsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("A", string.Empty),
                new KeyStringPair("B", "left,right")
            };

            const string expected = ",\"left,right\"";
            var actual = CsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CsvFile.Line(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(null as KeyStringDictionary));
        }

        [Fact]
        public void op_Line_IEnumerableString()
        {
            var obj = new List<string>
            {
                "123",
                "left,right"
            };

            const string expected = "123,\"left,right\"";
            var actual = CsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_IEnumerableString_whenEmptyValue()
        {
            var obj = new List<string>
            {
                string.Empty,
                "left,right"
            };

            const string expected = ",\"left,right\"";
            var actual = CsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_IEnumerableStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CsvFile.Line(new List<string>()));
        }

        [Fact]
        public void op_Line_IEnumerableStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(null as IEnumerable<string>));
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyStringDictionary()
        {
            var data = new[]
            {
                new KeyStringDictionary
                {
                    new KeyStringPair("A", "1"),
                    new KeyStringPair("B", "2")
                }
            };

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("example").ToFile("test.csv");
                new CsvFile(file).Save(FileMode.Create, data);

                var expected = "A,B{0}1,2{0}".FormatWith(Environment.NewLine);
                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyStringDictionaryEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("test.csv");
                file.CreateNew();

                new CsvFile(file).Save(FileMode.Create, new List<KeyStringDictionary>());

                file.Refresh();
                Assert.False(file.Exists);
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyStringDictionaryNull()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new CsvFile(temp.Info.ToDirectory("example").ToFile("test.csv"));

                Assert.Throws<ArgumentNullException>(() => obj.Save(FileMode.Create, null as IEnumerable<KeyStringDictionary>));
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyValuePairFileInfoKeyStringDictionary()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("test.csv");
                var data = new Dictionary<FileInfo, KeyStringDictionary>
                {
                    {
                        file, new KeyStringDictionary
                        {
                            new KeyStringPair("A", "1"),
                            new KeyStringPair("B", "2")
                        }
                        }
                };

                CsvFile.Save(FileMode.Create, data);

                var expected = "A,B{0}1,2{0}".FormatWith(Environment.NewLine);
                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<CsvFile>(p => p.Info)
                            .TypeIs<FileInfo>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}