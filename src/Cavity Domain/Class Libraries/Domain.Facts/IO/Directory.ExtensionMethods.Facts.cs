namespace Cavity.IO
{
    using System;
    using System.IO;
#if NET40
    using System.Numerics;
#endif
    using Xunit;

    public sealed class DirectoryExtensionMethodsFacts
    {
#if NET40
        [Fact]
        public void op_ToBigInteger_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToBigInteger());
        }

        [Fact]
        public void op_ToBigInteger_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("1234567890", true);

                BigInteger expected = 1234567890;
                var actual = file.ToBigInteger();

                Assert.Equal(expected, actual);
            }
        }
#endif

        [Fact]
        public void op_ToDate_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToDate());
        }

        [Fact]
        public void op_ToDate_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("1969-03-10", true);

                var expected = new Date(1969, MonthOfYear.March, 10);
                var actual = file.ToDate();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToInt32_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToInt32());
        }

        [Fact]
        public void op_ToInt32_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("1234567890", true);

                var expected = 1234567890;
                var actual = file.ToInt32();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToMonth_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToMonth());
        }

        [Fact]
        public void op_ToMonth_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("1969-03", true);

                var expected = new Month(1969, MonthOfYear.March);
                var actual = file.ToMonth();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToQuarter_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToQuarter());
        }

        [Fact]
        public void op_ToQuarter_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToDirectory("1969 Q1", true);

                var expected = new Quarter(1969, QuarterOfYear.Q1);
                var actual = file.ToQuarter();

                Assert.Equal(expected, actual);
            }
        }
    }
}