namespace Cavity.IO
{
    using System;
    using System.IO;

    using Xunit;

    public sealed class CurrentDirectoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CurrentDirectory>().DerivesFrom<object>()
                                                                .IsConcreteClass()
                                                                .IsUnsealed()
                                                                .HasDefaultConstructor()
                                                                .IsNotDecorated()
                                                                .Result);
        }

        [Fact]
        public void ctor()
        {
            var expected = Directory.GetCurrentDirectory();
            var actual = new CurrentDirectory().Info.FullName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                var actual = new CurrentDirectory(temp.Info).Info.FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ctor_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CurrentDirectory(null as DirectoryInfo));
        }

        [Fact]
        public void ctor_string()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                var actual = new CurrentDirectory(expected).Info.FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentException>(() => new CurrentDirectory(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CurrentDirectory(null as string));
        }
    }
}