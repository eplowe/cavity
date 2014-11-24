namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Collections;
    using Cavity.IO;
    using Xunit;

    public sealed class ExcelWorksheetFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ExcelWorksheet>()
                            .DerivesFrom<DataSheet>()
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
            using (var temp = new TempFile())
            {
                Assert.NotNull(new ExcelWorksheet(temp.Info));
            }
        }

        [Fact]
        public void ctor_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => new ExcelWorksheet(temp.Info.ToFile("missing.txt")));
                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcelWorksheet(null as FileInfo));
        }

        [Fact]
        public void ctor_string()
        {
            using (var temp = new TempFile())
            {
                Assert.NotNull(new ExcelWorksheet(temp.Info.FullName));
            }
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xls");

            var sheet = new ExcelWorksheet(file)
                            {
                                Title = "Sheet1$"
                            };
            foreach (var entry in sheet)
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenBinaryFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsb");

            var sheet = new ExcelWorksheet(file)
                            {
                                Title = "Sheet1$"
                            };
            foreach (var entry in sheet)
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenMultipleColumns()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("OneTwo.xlsx");

            var sheet = new ExcelWorksheet(file)
                            {
                                Title = "Sheet1$"
                            };
            foreach (var entry in sheet)
            {
                Assert.Equal("1", entry["one"]);
                Assert.Equal("2", entry["two"]);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenXmlFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            var sheet = new ExcelWorksheet(file)
                            {
                                Title = "Sheet1$"
                            };
            foreach (var entry in sheet)
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            IEnumerable enumerable = new ExcelWorksheet(file)
                                         {
                                             Title = "Sheet1$"
                                         };
            foreach (var entry in enumerable.Cast<KeyStringDictionary>())
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<ExcelWorksheet>(x => x.Info)
                            .TypeIs<FileInfo>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}