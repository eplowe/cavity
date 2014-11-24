#if !NET20 && !NET35

namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class IOrderedEnumerableOfFileSystemExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IOrderedEnumerableOfFileSystemExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableDirectoryInfoEmpty()
        {
            var obj = new List<DirectoryInfo>().OrderBy(x => x);

            obj.DeleteAllExceptLast();
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableDirectoryInfoMultiple()
        {
            using (var temp = new TempDirectory())
            {
                var t = temp;
                Serial.ForEach("abc", letter => t.Info.ToDirectory(letter, true));

                temp.Info.EnumerateDirectories().OrderBy(x => x.Name).DeleteAllExceptLast();

                const int expected = 1;
                var actual = temp.Info.EnumerateDirectories().Count();
                Assert.Equal(expected, actual);
                Assert.True(temp.Info.ToDirectory("c").Exists);
            }
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableDirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IOrderedEnumerable<DirectoryInfo>).DeleteAllExceptLast());
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableDirectoryInfoSingle()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToDirectory("example", true);

                temp.Info.EnumerateDirectories().OrderBy(x => x).DeleteAllExceptLast();

                Assert.True(temp.Info.ToDirectory("example").Exists);
            }
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableFileInfoEmpty()
        {
            var obj = new List<FileInfo>().OrderBy(x => x);

            obj.DeleteAllExceptLast();
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableFileInfoMultiple()
        {
            using (var temp = new TempDirectory())
            {
                var t = temp;
                Serial.ForEach("abc", letter => t.Info.ToFile(letter).Create("test"));

                temp.Info.EnumerateFiles().OrderBy(x => x.Name).DeleteAllExceptLast();

                const int expected = 1;
                var actual = temp.Info.EnumerateFiles().Count();
                Assert.Equal(expected, actual);
                Assert.True(temp.Info.ToFile("c").Exists);
            }
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableFileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IOrderedEnumerable<FileInfo>).DeleteAllExceptLast());
        }

        [Fact]
        public void op_DeleteAllExceptLast_IOrderedEnumerableFileInfoSingle()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToFile("example.txt").Create("test");

                temp.Info.EnumerateFiles().OrderBy(x => x).DeleteAllExceptLast();

                Assert.True(temp.Info.ToFile("example.txt").Exists);
            }
        }
    }
}

#endif