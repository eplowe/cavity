namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class FileSpecFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileSpec>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<FileInfo>>()
                            .Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void op_Load_stringEmpty(string name)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FileSpec(name));
        }

        [Fact]
        public void op_Load_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileSpec(null));
        }

        [Fact]
        public void op_Load_string_whenFile()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp
                    .Info
                    .ToFile("example.txt")
                    .AppendLine(string.Empty)
                    .FullName;

                var actual = new FileSpec(expected).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Load_string_whenFileMissing()
        {
            using (var temp = new TempDirectory())
            {
                var name = temp
                    .Info
                    .ToFile("example.txt")
                    .FullName;

                Assert.Throws<FileNotFoundException>(() => new FileSpec(name).ToList());
            }
        }

        [Fact]
        public void op_Load_string_whenFileSearch()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp
                    .Info
                    .ToFile("example.txt")
                    .AppendLine(string.Empty)
                    .FullName;

                var name = @"{0}\*.txt".FormatWith(temp.Info.FullName);

                var actual = new FileSpec(name).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Load_string_whenFileSearchTopDirectoryOnly()
        {
            using (var temp = new TempDirectory())
            {
                temp
                    .Info
                    .ToDirectory("child", true)
                    .ToFile("example.txt")
                    .AppendLine(string.Empty);

                var name = @"{0}\*.txt".FormatWith(temp.Info.FullName);

                Assert.Empty(new FileSpec(name));
            }
        }

        [Fact]
        public void op_Load_string_whenNestedFileSearch()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp
                    .Info
                    .ToDirectory("child", true)
                    .ToFile("example.txt")
                    .AppendLine(string.Empty)
                    .FullName;

                var name = @"{0}\**\*.txt".FormatWith(temp.Info.FullName);

                var actual = new FileSpec(name).First().FullName;

                Assert.Equal(expected, actual);
            }
        }
    }
}