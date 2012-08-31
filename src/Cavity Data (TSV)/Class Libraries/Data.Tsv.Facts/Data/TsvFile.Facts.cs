namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif

    using Cavity;
    using Cavity.Collections;
    using Cavity.IO;

    using Moq;

    using Xunit;

    public sealed class TsvFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TsvFile>()
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
            Assert.NotNull(new TsvFile(new FileInfo("test.tsv")));
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TsvFile(null as FileInfo));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TsvFile("test.tsv"));
        }

        [Fact]
        public void op_AsOfT()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");

                foreach (var item in new TsvFile(file.Info).As<TestTsvEntry>())
                {
                    Assert.Equal("value", item.Name);
                }
            }
        }

        [Fact]
        public void op_GetEnumerator()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");

                foreach (var item in new TsvFile(file.Info))
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
                foreach (var item in new TsvFile(file.Info))
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
                              new KeyStringPair("A", string.Empty), 
                              new KeyStringPair("B", string.Empty)
                          };

            const string expected = "A\tB";
            var actual = TsvFile.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Header_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TsvFile.Header(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Header_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Header(null));
        }

        [Fact]
        public void op_Header_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair(string.Empty, "x"), 
                              new KeyStringPair("ABC", "x")
                          };

            const string expected = "\tABC";
            var actual = TsvFile.Header(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");
                IEnumerable enumerable = new TsvFile(file.Info);
                foreach (var entry in enumerable.Cast<KeyStringDictionary>())
                {
                    Assert.Equal("value", entry["name"]);
                }
            }
        }

        [Fact]
        public void op_Line_IEnumerableString()
        {
            var obj = new List<string>
                          {
                              "123", 
                              "left,right"
                          };

            const string expected = "123\tleft,right";
            var actual = TsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_IEnumerableStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TsvFile.Line(new List<string>()));
        }

        [Fact]
        public void op_Line_IEnumerableStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(null as IEnumerable<string>));
        }

        [Fact]
        public void op_Line_IEnumerableString_whenEmptyValue()
        {
            var obj = new List<string>
                          {
                              string.Empty, 
                              "ABC"
                          };

            const string expected = "\tABC";
            var actual = TsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "XYZ")
                          };

            const string expected = "123\tXYZ";
            var actual = TsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionaryEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TsvFile.Line(new KeyStringDictionary()));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(null as KeyStringDictionary));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull_IListOfString()
        {
            var columns = new List<string>
                              {
                                  "A"
                              };
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(null, columns));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(null, "A\tC"));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "XYZ")
                          };

            const string expected = "123\tXYZ";
            var actual = TsvFile.Line(obj, "A,C".Split(',').ToList());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TsvFile.Line(new KeyStringDictionary(), new List<string>()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(new KeyStringDictionary(), null as IList<string>));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => TsvFile.Line(new KeyStringDictionary(), "A,B".Split(',').ToList()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "XYZ")
                          };

            const string expected = "123\tXYZ";
            var actual = TsvFile.Line(obj, "A\tC");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TsvFile.Line(new KeyStringDictionary(), string.Empty));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Line(new KeyStringDictionary(), null as string));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => TsvFile.Line(new KeyStringDictionary(), "A"));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_whenEmptyValue()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", string.Empty), 
                              new KeyStringPair("B", "XYZ")
                          };

            const string expected = "\tXYZ";
            var actual = TsvFile.Line(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Save_FileInfoExists_DataTable()
        {
            var table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            table.Columns.Add("A");
            table.Columns.Add("B");
            var row = table.NewRow();
            row["A"] = "A2";
            row["B"] = "B2";
            table.Rows.Add(row);

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.tsv");
                file.AppendLine("A\tB");
                file.AppendLine("A1\tB1");
                TsvFile.Save(file, table);

                var entries = new TsvFile(file).ToList();

                Assert.Equal("A1", entries.First()["A"]);
                Assert.Equal("B2", entries.Last()["B"]);
            }
        }

        [Fact]
        public void op_Save_FileInfoNull_DataTable()
        {
            var table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };

            Assert.Throws<ArgumentNullException>(() => TsvFile.Save(null, table));
        }

        [Fact]
        public void op_Save_FileInfo_DataTable()
        {
            var table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            table.Columns.Add("A");
            table.Columns.Add("B");
            var row = table.NewRow();
            row["A"] = "1";
            row["B"] = "2";
            table.Rows.Add(row);

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.tsv");

                TsvFile.Save(file, table);

                var expected = "A\tB{0}1\t2{0}".FormatWith(Environment.NewLine);
                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Save_FileInfo_DataTableNull()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => TsvFile.Save(temp.Info.ToFile("example.tsv"), null));

                // ReSharper restore AccessToDisposedClosure
            }
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
                var file = temp.Info.ToDirectory("example").ToFile("test.tsv");
                new TsvFile(file).Save(FileMode.Create, data);

                var expected = "A\tB{0}1\t2{0}".FormatWith(Environment.NewLine);
                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyStringDictionaryEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("test.tsv");
                file.CreateNew();

                new TsvFile(file).Save(FileMode.Create, new List<KeyStringDictionary>());

                file.Refresh();
                Assert.False(file.Exists);
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyStringDictionaryNull()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new TsvFile(temp.Info.ToDirectory("example").ToFile("test.tsv"));

                Assert.Throws<ArgumentNullException>(() => obj.Save(FileMode.Create, null as IEnumerable<KeyStringDictionary>));
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyValuePairFileInfoKeyStringDictionary()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("test.tsv");
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

                TsvFile.Save(FileMode.Create, data);

                var expected = "A\tB{0}1\t2{0}".FormatWith(Environment.NewLine);
                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Save_FileMode_IEnumerableKeyValuePairNull()
        {
            Assert.Throws<ArgumentNullException>(() => TsvFile.Save(FileMode.Create, null as IEnumerable<KeyValuePair<FileInfo, KeyStringDictionary>>));
        }

        [Fact]
        public void op_ToDataTable()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");
                var csv = new TsvFile(file.Info);
                foreach (DataRow row in csv.ToDataTable().Rows)
                {
                    Assert.Equal("value", row.Field<string>("name"));
                }
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<TsvFile>(p => p.Info)
                            .TypeIs<FileInfo>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}