namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity;
    using Cavity.Collections;
    using Cavity.IO;
    using Xunit;

    public sealed class ExcelWorksheetFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ExcelWorksheet>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IEnumerable<KeyStringDictionary>>()
                .Result);
        }

        [Fact]
        public void ctor_FileInfo_string()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("Default.xlsx");

            Assert.NotNull(new ExcelWorksheet(file, "Sheet1$"));
        }

        [Fact]
        public void ctor_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcelWorksheet(null, "Sheet1$"));
        }

        [Fact]
        public void ctor_FileInfo_stringNull()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("Default.xlsx");

            Assert.Throws<ArgumentNullException>(() => new ExcelWorksheet(file, null));
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            IEnumerable enumerable = new ExcelWorksheet(file, "Sheet1$");
            foreach (var entry in enumerable.Cast<KeyStringDictionary>())
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xls");

            foreach (var entry in new ExcelWorksheet(file, "Sheet1$"))
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

            foreach (var entry in new ExcelWorksheet(file, "Sheet1$"))
            {
                Assert.Equal("1", entry["one"]);
                Assert.Equal("2", entry["two"]);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenBinaryFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsb");

            foreach (var entry in new ExcelWorksheet(file, "Sheet1$"))
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenXmlFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            foreach (var entry in new ExcelWorksheet(file, "Sheet1$"))
            {
                Assert.Equal("value", entry["name"]);
            }
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator_whenFileMissing()
        {
            var file = new FileInfo("{0}.xlsx".FormatWith(Guid.NewGuid()));

            IEnumerable enumerable = new ExcelWorksheet(file, "Sheet1$");

            Assert.Throws<FileNotFoundException>(() => enumerable.Cast<KeyStringDictionary>().ToList());
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<ExcelWorksheet>(x => x.Info)
                .TypeIs<FileInfo>()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<ExcelWorksheet>(x => x.Name)
                .TypeIs<string>()
                .IsNotDecorated()
                .Result);
        }
    }
}