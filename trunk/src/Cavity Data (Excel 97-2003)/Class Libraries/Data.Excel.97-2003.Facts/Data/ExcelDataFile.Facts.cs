namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Xml;

    using Cavity;
    using Cavity.IO;

    using Xunit;

    public sealed class ExcelDataFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ExcelDataFile>()
                            .DerivesFrom<DataFile>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            using (var temp = new TempFile())
            {
                Assert.NotNull(new ExcelDataFile(temp.Info));
            }
        }

        [Fact]
        public void ctor_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => new ExcelDataFile(temp.Info.ToFile("missing.xls")));
                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcelDataFile(null));
        }

        [Fact]
        public void op_IEnumerable_GetEnumerator()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("{0}.xls".FormatWith(AlphaDecimal.Random()));
                new DirectoryInfo(Environment.CurrentDirectory).ToFile("Default.xls").CopyTo(file.FullName);

                var i = 1;
                foreach (var sheet in new ExcelDataFile(file))
                {
                    var expected = "Sheet" + XmlConvert.ToString(i++);
                    Assert.Equal(expected, sheet.Title);
                    Assert.Empty(sheet);
                }
            }
        }
    }
}