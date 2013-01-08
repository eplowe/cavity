namespace Cavity
{
    using Xunit;
    using Xunit.Extensions;

    public sealed class QuarterOfYearFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(QuarterOfYear).IsEnum);
        }

        [Theory]
        [InlineData(0, QuarterOfYear.None)]
        [InlineData(1, QuarterOfYear.Q1)]
        [InlineData(2, QuarterOfYear.Q2)]
        [InlineData(3, QuarterOfYear.Q3)]
        [InlineData(4, QuarterOfYear.Q4)]
        public void values(int expected,
                           QuarterOfYear quarter)
        {
            Assert.Equal(expected, (int)quarter);
        }
    }
}