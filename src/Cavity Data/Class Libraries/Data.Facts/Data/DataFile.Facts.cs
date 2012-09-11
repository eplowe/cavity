namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Cavity.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DataFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DataFile>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IEnumerable<IDataSheet>>()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            using (var temp = new TempFile())
            {
                Assert.NotNull(new DerivedDataFile(temp.Info));
            }
        }

        [Fact]
        public void ctor_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => new DerivedDataFile(temp.Info.ToFile("missing.txt")));
                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedDataFile(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            using (var temp = new TempFile())
            {
                Assert.Empty(new DerivedDataFile(temp.Info));
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<DataFile>(x => x.Info)
                            .IsNotDecorated()
                            .TypeIs<FileInfo>()
                            .Result);
        }

        [Fact]
        public void prop_Info_get()
        {
            using (var temp = new TempFile())
            {
                var expected = temp.Info;
                var actual = new DerivedDataFile(expected).Info;

                Assert.Same(expected, actual);
            }
        }

        [Fact]
        public void prop_Title()
        {
            Assert.True(new PropertyExpectations<DataFile>(x => x.Title)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .Result);
        }

        [Theory]
        [InlineData("Data", "Data")]
        [InlineData("Data", "Data.example")]
        [InlineData("", ".example")]
        public void prop_Title_get(string expected,
                                   string fileName)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(fileName).AppendLine(string.Empty);
                var actual = new DerivedDataFile(file).Title;

                Assert.Equal(expected, actual);
            }
        }
    }
}