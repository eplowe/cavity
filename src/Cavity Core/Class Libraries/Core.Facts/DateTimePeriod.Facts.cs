namespace Cavity
{
    using System;
    using System.Xml;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DateTimePeriodOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DateTimePeriod>()
                            .IsValueType()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DateTimePeriod());
        }

        [Fact]
        public void ctor_DateTime_DateTime()
        {
            Assert.NotNull(new DateTimePeriod(DateTime.MinValue, DateTime.MaxValue));
        }

        [Fact]
        public void ctor_DateTime_DateTime_whenArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DateTimePeriod(DateTime.MaxValue, DateTime.MinValue));
        }

        [Fact]
        public void prop_Beginning()
        {
            Assert.True(new PropertyExpectations<DateTimePeriod>(x => x.Beginning)
                            .IsNotDecorated()
                            .TypeIs<DateTime>()
                            .DefaultValueIs(DateTime.MinValue)
                            .Result);
        }

        [Theory]
        [InlineData("2011-12-31T12:00:00Z", "2012-01-01T12:00:00Z")]
        [InlineData("2012-01-01T12:00:00Z", "2012-01-01T12:00:00Z")]
        public void prop_Beginning_set(string beginning, 
                                       string ending)
        {
            var obj = new DateTimePeriod(DateTime.MinValue, XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));

            Assert.DoesNotThrow(() => obj.Beginning = XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc));
        }

        [Theory]
        [InlineData("2012-01-01T12:00:00Z", "2011-12-31T12:00:00Z")]
        public void prop_Beginning_set_whenArgumentOutOfRangeException(string beginning, 
                                                                       string ending)
        {
            var obj = new DateTimePeriod(DateTime.MinValue, XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Beginning = XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc));
        }

        [Fact]
        public void prop_Duration()
        {
            Assert.True(new PropertyExpectations<DateTimePeriod>(x => x.Duration)
                            .IsNotDecorated()
                            .TypeIs<TimeSpan>()
                            .DefaultValueIs(TimeSpan.Zero)
                            .Result);
        }

        [Fact]
        public void prop_Duration_get()
        {
            var expected = DateTime.MaxValue - DateTime.MinValue;
            var actual = new DateTimePeriod(DateTime.MinValue, DateTime.MaxValue).Duration;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Ending()
        {
            Assert.True(new PropertyExpectations<DateTimePeriod>(x => x.Ending)
                            .IsNotDecorated()
                            .TypeIs<DateTime>()
                            .DefaultValueIs(DateTime.MinValue)
                            .Set(DateTime.MinValue)
                            .Set(DateTime.MaxValue)
                            .Result);
        }

        [Theory]
        [InlineData("2011-12-31T12:00:00Z", "2012-01-01T12:00:00Z")]
        [InlineData("2012-01-01T12:00:00Z", "2012-01-01T12:00:00Z")]
        public void prop_Ending_set(string beginning, 
                                    string ending)
        {
            var obj = new DateTimePeriod(XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc), DateTime.MaxValue);

            Assert.DoesNotThrow(() => obj.Ending = XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));
        }

        [Theory]
        [InlineData("2012-01-01T12:00:00Z", "2011-12-31T12:00:00Z")]
        public void prop_Ending_set_whenArgumentOutOfRangeException(string beginning, 
                                                                    string ending)
        {
            var obj = new DateTimePeriod(XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc), DateTime.MaxValue);

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Ending = XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));
        }
    }
}