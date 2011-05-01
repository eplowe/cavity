namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class FileInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(FileInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Lines_FileInfo()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = new FileInfo(Path.Combine(temp.Info.FullName, Guid.NewGuid().ToString()));
                using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(expected);
                    }
                }

                foreach (var actual in file.Lines())
                {
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_Lines_FileInfoEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var file = new FileInfo(Path.Combine(temp.Info.FullName, Guid.NewGuid().ToString()));
                using (file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                {
                }

                Assert.Empty(file.Lines());
            }
        }

        [Fact]
        public void op_Lines_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = new FileInfo(Path.Combine(temp.Info.FullName, Guid.NewGuid().ToString()));

                Assert.Throws<FileNotFoundException>(() => file.Lines().ToArray());
            }
        }

        [Fact]
        public void op_Lines_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Lines().ToArray());
        }

        [Fact]
        public void op_ReadToEnd_FileInfo()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = new FileInfo(Path.Combine(temp.Info.FullName, Guid.NewGuid().ToString()));
                using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(expected);
                    }
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ReadToEnd_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = new FileInfo(Path.Combine(temp.Info.FullName, Guid.NewGuid().ToString()));
                Assert.Throws<FileNotFoundException>(() => file.ReadToEnd());
            }
        }

        [Fact]
        public void op_ReadToEnd_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).ReadToEnd());
        }
    }
}