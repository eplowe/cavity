namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class NameFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Name>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void op_Load_stringEmpty(string name)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Name.Load(name));
        }

        [Fact]
        public void op_Load_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Load(null));
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

                var actual = Name.Load(expected).Files.First().FullName;

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

                Assert.Throws<FileNotFoundException>(() => Name.Load(name));
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

                var actual = Name.Load(name).Files.First().FullName;

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

                Assert.Empty(Name.Load(name).Files);
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

                var actual = Name.Load(name).Files.First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<Name>(x => x.Files)
                            .TypeIs<IList<FileInfo>>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }
    }
}