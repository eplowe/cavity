namespace Cavity.Compression
{
    using System;
    using System.IO;

    using Cavity.IO;

    using Xunit;

    public sealed class GZipFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(GZip).IsStatic());
        }

        [Fact]
        public void op_Extract_FileInfoMissing_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var source = new FileInfo(AlphaDecimal.Random());

                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => GZip.Extract(source, temp.Info));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Extract_FileInfoNull_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => GZip.Extract(null, temp.Info));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Extract_FileInfo_DirectoryInfo()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var source = new FileInfo("example.txt.gz");
                var destination = temp.Info;

                var actual = GZip.Extract(source, destination).ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Extract_FileInfo_DirectoryInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var source = new FileInfo("example.txt.gz");
                var destination = temp.Info.ToDirectory(AlphaDecimal.Random());

                Assert.Throws<DirectoryNotFoundException>(() => GZip.Extract(source, destination));
            }
        }

        [Fact]
        public void op_Extract_FileInfo_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => GZip.Extract(new FileInfo("example.txt.gz"), null));
        }
    }
}