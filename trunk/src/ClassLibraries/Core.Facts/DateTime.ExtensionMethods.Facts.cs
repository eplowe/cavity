namespace Cavity
{
    using System;
    using Xunit;

    public sealed class DateTimeExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(DateTimeExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ToLocalTime_DateTime_TimeZoneInfo()
        {
            var obj = DateTime.UtcNow;

            var value = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

            var expected = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(obj, value.Id);
            var actual = obj.ToLocalTime(value);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToLocalTime_DateTime_TimeZoneInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => DateTime.UtcNow.ToLocalTime(null as TimeZoneInfo));
        }

        [Fact]
        public void op_ToLocalTime_DateTime_string()
        {
            var obj = DateTime.UtcNow;

            var value = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

            var expected = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(obj, value.Id);
            var actual = obj.ToLocalTime(value.StandardName);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToLocalTime_DateTime_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTime.UtcNow.ToLocalTime(string.Empty));
        }

        [Fact]
        public void op_ToLocalTime_DateTime_stringInvalid()
        {
            Assert.Throws<TimeZoneNotFoundException>(() => DateTime.UtcNow.ToLocalTime("Not a valid time zone"));
        }

        [Fact]
        public void op_ToLocalTime_DateTime_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => DateTime.UtcNow.ToLocalTime(null as string));
        }
    }
}