namespace Cavity
{
    using System;
    using Xunit;
    using Xunit.Extensions;

    public sealed class TimeSpanExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(TimeSpanExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData("           ", 0, 0, 0)]
        [InlineData("         1s", 0, 0, 1)]
        [InlineData("        21s", 0, 0, 21)]
        [InlineData("     3m 21s", 0, 3, 21)]
        [InlineData("    43m 21s", 0, 43, 21)]
        [InlineData(" 5h 43m 21s", 5, 43, 21)]
        [InlineData("65h 43m 21s", 65, 43, 21)]
        [InlineData("765h 43m 21s", 765, 43, 21)]
        [InlineData("65h 43m    ", 65, 43, 0)]
        [InlineData("65h        ", 65, 0, 0)]
        [InlineData("    43m    ", 0, 43, 0)]
        public void op_PrettyPrint_TimeSpan(string expected,
                                            int hours,
                                            int minutes,
                                            int seconds)
        {
            var actual = new TimeSpan(hours, minutes, seconds).PrettyPrint();

            Assert.Equal(expected, actual);
        }
    }
}