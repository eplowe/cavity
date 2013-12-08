namespace Cavity
{
    using Xunit;

    public sealed class TemporalExtensionMethodsFacts
    {
        [Fact]
        public void op_ToDate_string()
        {
            var expected = new Date(2010, 11, 12);
            var actual = "2010-11-12".ToDate();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToMonth_string()
        {
            var expected = new Month(2010, 11);
            var actual = "2010-11".ToMonth();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToQuarter_string()
        {
            var expected = new Quarter(2010, QuarterOfYear.Q4);
            var actual = "2010 Q4".ToQuarter();

            Assert.Equal(expected, actual);
        }
    }
}