namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Cavity.Collections;
    using Cavity.Data.Sharding;
    using Cavity.Data.Transformation;
    using Cavity.IO;

    using Moq;

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
        public void op_AsOfT()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");

                foreach (var item in new CsvFile(file.Info).As<TestCsvEntry>())
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
        public void op_IEnumerable_GetEnumerator()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");
                IEnumerable enumerable = new CsvFile(file.Info);
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

            const string expected = "123,\"left,right\"";
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
        public void op_Line_KeyStringDictionaryNull_IListOfString()
        {
            var columns = new List<string>
                              {
                                  "A"
                              };
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(null, columns));
        }

        [Fact]
        public void op_Line_KeyStringDictionaryNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(null, "A,C"));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "left,right")
                          };

            const string expected = "123,\"left,right\"";
            var actual = CsvFile.Line(obj, "A,C".Split(',').ToList());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CsvFile.Line(new KeyStringDictionary(), new List<string>()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(new KeyStringDictionary(), null as IList<string>));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_IListOfString_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => CsvFile.Line(new KeyStringDictionary(), "A,B".Split(',').ToList()));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string()
        {
            var obj = new KeyStringDictionary
                          {
                              new KeyStringPair("A", "123"), 
                              new KeyStringPair("B", "ignore"), 
                              new KeyStringPair("C", "left,right")
                          };

            const string expected = "123,\"left,right\"";
            var actual = CsvFile.Line(obj, "A,C");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CsvFile.Line(new KeyStringDictionary(), string.Empty));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Line(new KeyStringDictionary(), null as string));
        }

        [Fact]
        public void op_Line_KeyStringDictionary_string_whenColumnsNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => CsvFile.Line(new KeyStringDictionary(), "A"));
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
                var file = temp.Info.ToFile("example.csv");
                file.AppendLine("A,B");
                file.AppendLine("A1,B1");
                CsvFile.Save(file, table);

                var entries = new CsvFile(file).ToList();

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

            Assert.Throws<ArgumentNullException>(() => CsvFile.Save(null, table));
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
                var file = temp.Info.ToFile("example.csv");

                CsvFile.Save(file, table);

                var expected = "A,B{0}1,2{0}".FormatWith(Environment.NewLine);
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
                Assert.Throws<ArgumentNullException>(() => CsvFile.Save(temp.Info.ToFile("example.csv"), null));

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
        public void op_Save_FileMode_IEnumerableKeyValuePairNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvFile.Save(FileMode.Create, null as IEnumerable<KeyValuePair<FileInfo, KeyStringDictionary>>));
        }

        [Fact]
        public void op_Shard_IIdentifyShard()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("EXAMPLE");
                file.Info.AppendLine("abc");
                file.Info.AppendLine("xyz");
                var csv = new CsvFile(file.Info);
                var tables = csv.Shard(new ShardByValue("EXAMPLE")).ToList();

                Assert.Equal("abc", tables.First().TableName);
                Assert.Equal("abc", tables.First().Rows[0].Field<string>("EXAMPLE"));

                Assert.Equal("xyz", tables.Last().TableName);
                Assert.Equal("xyz", tables.Last().Rows[0].Field<string>("EXAMPLE"));
            }
        }

        [Fact]
        public void op_Shard_IIdentifyShardNull()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");
                var csv = new CsvFile(file.Info);

                Assert.Throws<ArgumentNullException>(() => csv.Shard(null).ToList());
            }
        }

        [Fact]
        public void op_Shard_IIdentifyShard_whenEmpty()
        {
            using (var file = new TempFile())
            {
                var csv = new CsvFile(file.Info);
                var identifier = new Mock<IIdentifyShard>().Object;

                Assert.Empty(csv.Shard(identifier).ToList());
            }
        }

        [Fact]
        public void op_ToDataTable()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("name");
                file.Info.AppendLine("value");
                var csv = new CsvFile(file.Info);
                foreach (DataRow row in csv.ToDataTable().Rows)
                {
                    Assert.Equal("value", row.Field<string>("name"));
                }
            }
        }

        [Fact]
        public void op_Transform_ITransformEntries()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("A,B,C");
                file.Info.AppendLine("a,b,c");
                file.Info.AppendLine("x,y,z");
                var csv = new CsvFile(file.Info);

                var entries = csv.Transform(new ConcatenationTransformer()).ToList();

                Assert.Equal("abc", entries.First()["CONCAT"]);
                Assert.Equal("xyz", entries.Last()["CONCAT"]);
            }
        }

        [Fact]
        public void op_Transform_ITransformEntriesNull()
        {
            using (var file = new TempFile())
            {
                var csv = new CsvFile(file.Info);

                Assert.Throws<ArgumentNullException>(() => csv.Transform(null).ToList());
            }
        }

        [Fact]
        public void op_Transform_ITransformEntriesOfT()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("A,B,C");
                file.Info.AppendLine("1,2,3");
                file.Info.AppendLine("4,5,0");
                var csv = new CsvFile(file.Info);

                var entries = csv.Transform(new AdditionTransformer()).ToList();

                Assert.Equal(6, entries.First());
                Assert.Equal(9, entries.Last());
            }
        }

        [Fact]
        public void op_Transform_ITransformEntriesOfTNull()
        {
            using (var file = new TempFile())
            {
                var csv = new CsvFile(file.Info);

                Assert.Throws<ArgumentNullException>(() => csv.Transform<int>(null).ToList());
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