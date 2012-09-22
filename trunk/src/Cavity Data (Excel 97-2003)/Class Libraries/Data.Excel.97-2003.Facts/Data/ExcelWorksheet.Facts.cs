namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Cavity;
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
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
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
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("{0}.xls".FormatWith(AlphaDecimal.Random()));
                new DirectoryInfo(Environment.CurrentDirectory).ToFile("Default.xls").CopyTo(file.FullName);

                for (var i = 1; i < 4; i++)
                {
                    var sheet = new ExcelWorksheet(file)
                    {
                        Title = "Sheet" + XmlConvert.ToString(i)
                    };
                    Assert.Empty(sheet);
                }
            }
        }

        [Fact]
        public void op_GetEnumerator_whenSheet1()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("{0}.xls".FormatWith(AlphaDecimal.Random()));
                new DirectoryInfo(Environment.CurrentDirectory).ToFile("Example.xls").CopyTo(file.FullName);

                var sheet = new ExcelWorksheet(file)
                {
                    Title = "Sheet1"
                };

                var data = sheet.ToList();
                Assert.Equal(3, data.Count);

                Assert.Equal("Top Left,123,123.45,true", Csv.Line(data[0]));
                Assert.Equal("1969-03-10,456%,£456.78,", Csv.Line(data[1]));
                Assert.Equal("1969-03-10T01:02:03Z,,,false", Csv.Line(data[2]));
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