namespace Cavity.IO
{
    using System;
    using System.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class FileInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            ////Assert.True(typeof(FileInfoExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData(false, "", "")]
        [InlineData(false, "example", "example")]
        [InlineData(false, "\r\n", "\r\n")]
        [InlineData(false, "example\r\n", "example\r\n")]
        [InlineData(true, "\r\n", "\n")]
        [InlineData(true, "example\r\n", "example\n")]
        [InlineData(true, "\r\n\r\n", "\n\n")]
        [InlineData(true, "example\r\n\r\n", "example\n\n")]
        public void op_FixNewLine_FileInfo(bool result,
                                           string expected, 
                                           string value)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").Append(value);

                Assert.Equal(result, file.FixNewLine());

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_FixNewLine_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => temp.Info.ToFile("missing.txt").FixNewLine());

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_FixNewLine_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).FixNewLine());
        }
    }
}