namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DateTimePeriodOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DateTimePeriod>().IsValueType()
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
        public void ctor_Date_Date()
        {
            Assert.NotNull(new DateTimePeriod(Date.MinValue, Date.MaxValue));
        }

        [Fact]
        public void ctor_Date_Date_whenArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DateTimePeriod(Date.MaxValue, Date.MinValue));
        }

        [Theory]
        [InlineData(false, "2012-10-31")]
        [InlineData(true, "2012-11-01")]
        [InlineData(true, "2012-11-15")]
        [InlineData(true, "2012-11-30")]
        [InlineData(false, "2012-12-01")]
        public void op_Contains_Date(bool expected, 
                                     string value)
        {
            var period = new DateTimePeriod(XmlConvert.ToDateTime("2012-11-01T00:00:00Z", XmlDateTimeSerializationMode.Utc), 
                                            XmlConvert.ToDateTime("2012-11-30T23:59:59Z", XmlDateTimeSerializationMode.Utc));
            var actual = period.Contains(Date.FromString(value));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "2012-10-31T23:59:59Z")]
        [InlineData(true, "2012-11-01T00:00:00Z")]
        [InlineData(true, "2012-11-15T12:11:10Z")]
        [InlineData(true, "2012-11-30T23:59:59Z")]
        [InlineData(false, "2012-12-01T00:00:00Z")]
        public void op_Contains_DateTime(bool expected, 
                                         string value)
        {
            var period = new DateTimePeriod(XmlConvert.ToDateTime("2012-11-01T00:00:00Z", XmlDateTimeSerializationMode.Utc), 
                                            XmlConvert.ToDateTime("2012-11-30T23:59:59Z", XmlDateTimeSerializationMode.Utc));
            var actual = period.Contains(XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Utc));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "2012-10")]
        [InlineData(true, "2012-11")]
        [InlineData(false, "2012-12")]
        public void op_Contains_Month(bool expected, 
                                      string value)
        {
            var period = new DateTimePeriod(XmlConvert.ToDateTime("2012-11-01T00:00:00Z", XmlDateTimeSerializationMode.Utc), 
                                            XmlConvert.ToDateTime("2012-11-30T23:59:59Z", XmlDateTimeSerializationMode.Utc));
            var actual = period.Contains(Month.FromString(value));

            Assert.Equal(expected, actual);
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
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "propEnding", Justification = "This is spelling is intended.")]
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
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "propEnding", Justification = "This is spelling is intended.")]
        public void prop_Ending_set(string beginning, 
                                    string ending)
        {
            var obj = new DateTimePeriod(XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc), DateTime.MaxValue);

            Assert.DoesNotThrow(() => obj.Ending = XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));
        }

        [Theory]
        [InlineData("2012-01-01T12:00:00Z", "2011-12-31T12:00:00Z")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "propEnding", Justification = "This is spelling is intended.")]
        public void prop_Ending_set_whenArgumentOutOfRangeException(string beginning, 
                                                                    string ending)
        {
            var obj = new DateTimePeriod(XmlConvert.ToDateTime(beginning, XmlDateTimeSerializationMode.Utc), DateTime.MaxValue);

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Ending = XmlConvert.ToDateTime(ending, XmlDateTimeSerializationMode.Utc));
        }
    }
}