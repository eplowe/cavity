namespace Cavity.IO
{
    using System;
    using System.IO;

    using Xunit;

    public sealed class FileSystemInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(FileSystemInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_CombineAsDirectory_FileSystemInfoNull_objects()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileSystemInfo).CombineAsDirectory(1, 2, 3));
        }

        [Fact]
        public void op_CombineAsDirectory_FileSystemInfo_objects()
        {
            const string expected = @"C:\1\2\3";
            var actual = new DirectoryInfo(@"C:\").CombineAsDirectory(1, 2, 3);

            Assert.IsType<DirectoryInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_CombineAsDirectory_FileSystemInfo_objectsEmpty()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).CombineAsDirectory();

            Assert.IsType<DirectoryInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_CombineAsDirectory_FileSystemInfo_objectsNull()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).CombineAsDirectory(null as object[]);

            Assert.IsType<DirectoryInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_CombineAsFile_FileSystemInfoNull_objects()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileSystemInfo).CombineAsFile(1, 2, 3));
        }

        [Fact]
        public void op_CombineAsFile_FileSystemInfo_objects()
        {
            const string expected = @"C:\1\2\3\example.txt";
            var actual = new DirectoryInfo(@"C:\").CombineAsFile(1, 2, 3, "example.txt");

            Assert.IsType<FileInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_CombineAsFile_FileSystemInfo_objectsEmpty()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).CombineAsFile();

            Assert.IsType<FileInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_CombineAsFile_FileSystemInfo_objectsNull()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).CombineAsFile(null as object[]);

            Assert.IsType<FileInfo>(actual);
            Assert.Equal(expected, actual.FullName);
        }

        [Fact]
        public void op_Combine_FileSystemInfoNull_objects()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileSystemInfo).Combine(1, 2, 3));
        }

        [Fact]
        public void op_Combine_FileSystemInfo_objects()
        {
            const string expected = @"C:\1\2\3";
            var actual = new DirectoryInfo(@"C:\").Combine(1, 2, 3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Combine_FileSystemInfo_objectsEmpty()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).Combine();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Combine_FileSystemInfo_objectsNull()
        {
            const string expected = @"C:\";
            var actual = new DirectoryInfo(expected).Combine(null as object[]);

            Assert.Equal(expected, actual);
        }
    }
}