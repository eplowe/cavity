namespace Cavity
{
    using Xunit;
    using Xunit.Extensions;

    public sealed class MonthOfYearFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(MonthOfYear).IsEnum);
        }

        [Theory]
        [InlineData(0, MonthOfYear.None)]
        [InlineData(1, MonthOfYear.January)]
        [InlineData(2, MonthOfYear.February)]
        [InlineData(3, MonthOfYear.March)]
        [InlineData(4, MonthOfYear.April)]
        [InlineData(5, MonthOfYear.May)]
        [InlineData(6, MonthOfYear.June)]
        [InlineData(7, MonthOfYear.July)]
        [InlineData(8, MonthOfYear.August)]
        [InlineData(9, MonthOfYear.September)]
        [InlineData(10, MonthOfYear.October)]
        [InlineData(11, MonthOfYear.November)]
        [InlineData(12, MonthOfYear.December)]
        public void values(int expected, 
                           MonthOfYear month)
        {
            Assert.Equal(expected, (int)month);
        }
    }
}