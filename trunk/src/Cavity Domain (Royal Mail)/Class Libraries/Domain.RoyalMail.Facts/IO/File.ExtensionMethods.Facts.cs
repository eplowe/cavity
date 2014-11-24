namespace Cavity.IO
{
    using System;
    using System.IO;
    using Cavity.Models;
    using Xunit;

    public sealed class FileExtensionMethodsFacts
    {
        [Fact]
        public void op_ToPostcode_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("AB12ZZ.txt").CreateNew(string.Empty);

                BritishPostcode expected = "AB1 2ZZ";
                var actual = file.ToPostcode();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToPostcode_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).ToPostcode());
        }
    }
}