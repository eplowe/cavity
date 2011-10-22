namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity;
    using Cavity.IO;
    using Xunit;

    public sealed class ExcelFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ExcelFile>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IEnumerable<ExcelWorksheet>>()
                .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("Default.xls");

            Assert.NotNull(new ExcelFile(file));
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcelFile(null));
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            IEnumerable enumerable = new ExcelFile(file);
            foreach (var entry in enumerable.Cast<ExcelWorksheet>())
            {
                Assert.Equal("Sheet1$", entry.Name);
            }
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xls");

            foreach (var entry in new ExcelFile(file))
            {
                Assert.Equal("Sheet1$", entry.Name);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenBinaryFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsb");

            foreach (var entry in new ExcelFile(file))
            {
                Assert.Equal("Sheet1$", entry.Name);
            }
        }

        [Fact]
        public void op_GetEnumerator_whenXmlFormat()
        {
            var file = new DirectoryInfo(Environment.CurrentDirectory)
                .ToDirectory("Spreadsheets")
                .ToFile("NameValue.xlsx");

            foreach (var entry in new ExcelFile(file))
            {
                Assert.Equal("Sheet1$", entry.Name);
            }
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator_whenFileMissing()
        {
            var file = new FileInfo("{0}.xlsx".FormatWith(Guid.NewGuid()));

            IEnumerable enumerable = new ExcelFile(file);

            Assert.Throws<FileNotFoundException>(() => enumerable.Cast<ExcelWorksheet>().ToList());
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<ExcelFile>(x => x.Info)
                .TypeIs<FileInfo>()
                .IsNotDecorated()
                .Result);
        }
    }
}