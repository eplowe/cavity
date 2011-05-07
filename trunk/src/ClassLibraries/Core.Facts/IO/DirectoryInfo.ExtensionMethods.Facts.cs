namespace Cavity.IO
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class DirectoryInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(DirectoryInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToDirectory("example"));
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_object()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example";

                var expected = Path.Combine(temp.Info.FullName, name);

                var actual = temp.Info.ToDirectory(name);

                Assert.False(actual.Exists);
                Assert.Equal(expected, actual.FullName);
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_objectNull()
        {
            using (var temp = new TempDirectory())
            {
                Assert.Throws<ArgumentNullException>(() => temp.Info.ToDirectory(null));
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_object_bool()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example";

                var expected = Path.Combine(temp.Info.FullName, name);

                var actual = temp.Info.ToDirectory(name, true);

                Assert.True(actual.Exists);
                Assert.Equal(expected, actual.FullName);
            }
        }

        [Fact]
        public void op_ToFile_DirectoryInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToFile("example.txt"));
        }

        [Fact]
        public void op_ToFile_DirectoryInfo_object()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example.txt";

                var expected = Path.Combine(temp.Info.FullName, name);
                var actual = temp.Info.ToFile(name).FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToFile_DirectoryInfo_objectNull()
        {
            using (var temp = new TempDirectory())
            {
                Assert.Throws<ArgumentNullException>(() => temp.Info.ToFile(null));
            }
        }
    }
}