namespace Cavity
{
    using System;
    using Cavity;
    using Xunit;
    using Xunit.Extensions;

    public sealed class PercentageExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(PercentageExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("0", "99", "0")]
        [InlineData("50", "2", "4")]
        [InlineData("150", "3", "2")]
        [InlineData("-50.0", "2", "-4")]
        [InlineData("33.333333333333333333333333330", "3", "9")]
        public void op_Percent_decimal(string expected, string value, string total)
        {
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().Percent(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().Percent(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().Percent(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().Percent(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().Percent(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().Percent(total.To<short>()));
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("0", "100", "100")]
        [InlineData("0", "-50", "-50")]
        [InlineData("50", "50", "75")]
        [InlineData("-50", "-50", "-75")]
        [InlineData("-50", "100", "50")]
        [InlineData("50", "-100", "-50")]
        [InlineData("200", "-50", "50")]
        [InlineData("-200", "50", "-50")]
        public void op_PercentageChange_decimal(string expected, string before, string after)
        {
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<decimal>().PercentageChange(after.To<short>()));

            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<double>().PercentageChange(after.To<short>()));

            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<float>().PercentageChange(after.To<short>()));

            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<int>().PercentageChange(after.To<short>()));

            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<long>().PercentageChange(after.To<short>()));

            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<double>()));
            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<float>()));
            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<int>()));
            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<long>()));
            Assert.Equal(expected.To<decimal>(), before.To<short>().PercentageChange(after.To<short>()));
        }

        [Theory]
        [InlineData("0", "0", "0")]
        [InlineData("0", "0", "99")]
        [InlineData("2", "50", "4")]
        [InlineData("3", "150", "2")]
        [InlineData("-2", "50", "-4")]
        [InlineData("2.70", "30", "9")]
        [InlineData("-50", "-50", "100")]
        [InlineData("-50", "100", "-50")]
        public void op_PercentageOf_decimal(string expected, string value, string total)
        {
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<decimal>().PercentageOf(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<double>().PercentageOf(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<float>().PercentageOf(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<int>().PercentageOf(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<long>().PercentageOf(total.To<short>()));

            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<decimal>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<double>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<float>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<int>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<long>()));
            Assert.Equal(expected.To<decimal>(), value.To<short>().PercentageOf(total.To<short>()));
        }
    }
}