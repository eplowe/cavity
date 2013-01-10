namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DateFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Date>().IsValueType()
                                                    .Serializable()
                                                    .IsDecoratedWith<ImmutableObjectAttribute>()
                                                    .Implements<IComparable>()
                                                    .Implements<IComparable<Date>>()
                                                    .Implements<IEquatable<Date>>()
                                                    .Implements<IChangeDate>()
                                                    .Implements<IGetNextDate>()
                                                    .Implements<IGetPreviousDate>()
                                                    .Implements<IGetTimeZone<Date>>()
                                                    .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Date());
        }

        [Fact]
        public void ctor_DateTime()
        {
            Assert.NotNull(new Date(DateTime.Today));
        }

        [Fact]
        [Comment("Bug discovered where British Summer Time caused Date to misbehave.")]
        public void ctor_DateTime_whenLocalTime()
        {
            Date expected = "2012-03-26";
            var actual = new Date(new DateTime(2012, 03, 25, 2, 1, 0, DateTimeKind.Local)).ToDateTime().AddDays(1).ToDate();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_Month_int()
        {
            Assert.NotNull(new Date(new Month(1999, MonthOfYear.May), 31));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Date(1999, 12, 31);
            Date actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Date(1999, 12, 31));
                stream.Position = 0;
                actual = (Date)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_int_MonthOfYear_int()
        {
            Assert.NotNull(new Date(1999, MonthOfYear.May, 31));
        }

        [Fact]
        public void ctor_int_int0_int()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Date(1999, 0, 31));
        }

        [Fact]
        public void ctor_int_int13_int()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Date(1999, 13, 31));
        }

        [Fact]
        public void ctor_int_int_int()
        {
            Assert.NotNull(new Date(1999, 12, 31));
        }

        [Fact]
        public void ctor_int_int_int0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Date(1999, 12, 0));
        }

        [Fact]
        public void ctor_int_int_int32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Date(1999, 12, 32));
        }

        [Fact]
        public void opDecrement()
        {
            var expected = Date.Today.LocalTime.AddDays(-1);
            var actual = Date.Today.LocalTime;
            actual--;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_Date_Date()
        {
            var obj = new Date();
            var comparand = new Date();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreaterThanOrEqual_Date_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            Assert.True(two >= one);
            Assert.True(two >= "2000-01-01");
        }

        [Fact]
        public void opGreaterThan_Date_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            Assert.True(two > one);
        }

        [Fact]
        public void opImplicit_Date_string()
        {
            var expected = new Date(1999, 12, 31);
            Date actual = "1999-12-31";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Date()
        {
            const string expected = "1999-12-31";
            string actual = new Date(1999, 12, 31);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIncrement()
        {
            var expected = Date.Today.LocalTime.AddDays(1);
            var actual = Date.Today.LocalTime;
            actual++;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Date_Date()
        {
            var obj = new Date();
            var comparand = new Date();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLessThan_Date_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            Assert.True(one < two);
        }

        [Fact]
        public void opLesserThanOrEqual_Date_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            Assert.True(one <= two);
            Assert.True(two <= "2000-01-01");
        }

        [Theory]
        [InlineData("2012-03-27", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2012-03-25", "2012-03-26", -1)]
        public void op_AddDays_int(string expected, 
                                   string date, 
                                   int value)
        {
            var actual = ((Date)date).AddDays(value);

            Assert.Equal((Date)expected, actual);
        }

        [Theory]
        [InlineData("2012-04-26", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2012-02-26", "2012-03-26", -1)]
        public void op_AddMonths_int(string expected, 
                                     string date, 
                                     int value)
        {
            var actual = ((Date)date).AddMonths(value);

            Assert.Equal((Date)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddMonths_int_whenArgumentOutOfRangeException(int value)
        {
            var date = Date.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddMonths(value));
        }

        [Theory]
        [InlineData("2012-06-26", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2011-12-26", "2012-03-26", -1)]
        public void op_AddQuarters_int(string expected, 
                                       string date, 
                                       int value)
        {
            var actual = ((Date)date).AddQuarters(value);

            Assert.Equal((Date)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddQuarters_int_whenArgumentOutOfRangeException(int value)
        {
            var date = Date.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddQuarters(value));
        }

        [Theory]
        [InlineData("2012-04-02", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2012-03-19", "2012-03-26", -1)]
        [InlineData("2012-03-12", "2012-03-26", -2)]
        public void op_AddWeeks_int(string expected, 
                                    string date, 
                                    int value)
        {
            var actual = ((Date)date).AddWeeks(value);

            Assert.Equal((Date)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddWeeks_int_whenArgumentOutOfRangeException(int value)
        {
            var date = Date.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddWeeks(value));
        }

        [Theory]
        [InlineData("2013-03-26", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2011-03-26", "2012-03-26", -1)]
        public void op_AddYears_int(string expected, 
                                    string date, 
                                    int value)
        {
            var actual = ((Date)date).AddYears(value);

            Assert.Equal((Date)expected, actual);
        }

        [Fact]
        public void op_Change_Day_int()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-12";
            var actual = value.Change.Day(12);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Change_Month_int()
        {
            Date value = "2012-11-09";

            Date expected = "2012-06-09";
            var actual = value.Change.Month(6);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Change_To_MonthOfYear()
        {
            Date value = "2012-11-09";

            Date expected = "2012-05-09";
            var actual = value.Change.To(MonthOfYear.May);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Change_Year_int()
        {
            Date value = "2012-11-09";

            Date expected = "1999-11-09";
            var actual = value.Change.Year(1999);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "2012-05-10T14:30:00Z")]
        [InlineData(0, "2012-05-30T12:00:00Z")]
        [InlineData(-1, "2012-06-10T14:30:00Z")]
        public void op_CompareTo_DateTime(int expected, 
                                          string date)
        {
            var obj = new Date(2012, 5, 30);
            var other = XmlConvert.ToDateTime(date, XmlDateTimeSerializationMode.Utc);

            Assert.Equal(expected, obj.CompareTo(other));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            Date one = "1999-12-31";
            object two = (Date)"2000-01-01";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Date().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Date().CompareTo(null as object));
        }

        [Fact]
        public void op_Compare_Date_Date()
        {
            var expected = DateTime.Compare(new DateTime(1999, 12, 31), new DateTime(2000, 1, 1));
            var actual = Date.Compare("1999-12-31", "2000-01-01");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Decrement()
        {
            var expected = Date.Today.LocalTime.AddDays(-1);
            var actual = Date.Today.LocalTime.Decrement();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_Date()
        {
            var obj = new Date();

            Assert.True(new Date().Equals(obj));
        }

        [Theory]
        [InlineData(false, 29)]
        [InlineData(true, 30)]
        public void op_Equals_DateTime(bool expected, 
                                       int day)
        {
            var obj = new Date(2012, 5, 30);
            var other = new DateTime(2012, 5, day, 12, 0, 0);

            Assert.Equal(expected, obj.Equals(other));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Date();

            Assert.True(new Date().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Date(DateTime.Now);

            Assert.False(new Date().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.Throws<InvalidCastException>(() => new Date().Equals(obj));

            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Date().Equals(null as object));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new Date(1999, 12, 31);
            var actual = Date.FromString("1999-12-31");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Date.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = DateTime.Today.GetHashCode();
            var actual = new Date(DateTime.Now).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Date(1999, 12, 31);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Date), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "12/31/1999 00:00:00";

            ISerializable value = new Date(1999, 12, 31);

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Increment()
        {
            var expected = Date.Today.LocalTime.AddDays(1);
            var actual = Date.Today.LocalTime.Increment();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime()
        {
            var expected = DateTime.MinValue.Date;
            var actual = new Date().ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime_whenValue()
        {
            var expected = DateTime.Today;
            var actual = new Date(DateTime.Now).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToMonth()
        {
            Month expected = "2012-03";
            var actual = new Date(2012, 3, 10).ToMonth();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "0001-01-01";
            var actual = new Date().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenValue()
        {
            const string expected = "1999-12-31";
            var actual = new Date(1999, 12, 31).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Calendar_Month()
        {
            var date = new Date(2012, 11, 10);

            var expected = new DateTimePeriod
                               {
                                   Ending = new DateTime(2012, 11, 30), 
                                   Beginning = new DateTime(2012, 11, 1)
                               };
            var actual = date.Calendar.Month;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Calendar_Week()
        {
            var date = new Date(2012, 11, 10);

            var expected = new DateTimePeriod
                               {
                                   Ending = new DateTime(2012, 11, 11), 
                                   Beginning = new DateTime(2012, 11, 5)
                               };
            var actual = date.Calendar.Week;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Calendar_Week_whenMonday()
        {
            var date = new Date(2012, 11, 12);

            var expected = new DateTimePeriod
                               {
                                   Ending = new DateTime(2012, 11, 18), 
                                   Beginning = new DateTime(2012, 11, 12)
                               };
            var actual = date.Calendar.Week;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Calendar_Week_whenSunday()
        {
            var date = new Date(2012, 11, 4);

            var expected = new DateTimePeriod
                               {
                                   Ending = new DateTime(2012, 11, 4), 
                                   Beginning = new DateTime(2012, 10, 29)
                               };
            var actual = date.Calendar.Week;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Calendar_Year()
        {
            var date = new Date(2012, 11, 10);

            var expected = new DateTimePeriod
                               {
                                   Ending = new DateTime(2012, 12, 31), 
                                   Beginning = new DateTime(2012, 1, 1)
                               };
            var actual = date.Calendar.Year;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Change()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Change)
                            .TypeIs<IChangeDate>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Day()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Day)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_DayOfWeek()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.DayOfWeek)
                            .TypeIs<DayOfWeek>()
                            .DefaultValueIs(DayOfWeek.Monday)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_DayOfYear()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.DayOfYear)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_DaysInMonth()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.DaysInMonth)
                            .TypeIs<int>()
                            .DefaultValueIs(31)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_FirstOfMonth()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.FirstOfMonth)
                            .TypeIs<Date>()
                            .DefaultValueIs(new Date())
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsDaylightSavingTime()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.IsDaylightSavingTime)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsLeapYear()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.IsLeapYear)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_LastOfMonth()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.LastOfMonth)
                            .TypeIs<Date>()
                            .IsNotDecorated()
                            .DefaultValueIs(new Date(1, 1, 31))
                            .Result);
        }

        [Fact]
        public void prop_MaxValue()
        {
            var expected = DateTime.MaxValue.ToDate();
            var actual = Date.MaxValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MinValue()
        {
            Date expected = DateTime.MinValue.ToDate();
            var actual = Date.MinValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Month()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Month)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_MonthOfYear()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.MonthOfYear)
                            .TypeIs<MonthOfYear>()
                            .DefaultValueIs(MonthOfYear.January)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Next()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Next)
                            .TypeIs<IGetNextDate>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Next_April()
        {
            Date value = "2012-11-09";

            Date expected = "2013-04-09";
            var actual = value.Next.April;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_August()
        {
            Date value = "2012-11-09";

            Date expected = "2013-08-09";
            var actual = value.Next.August;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Day()
        {
            Date value = "2012-08-24";

            Date expected = "2012-08-25";
            var actual = value.Next.Day;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_December()
        {
            Date value = "2012-11-09";

            Date expected = "2012-12-09";
            var actual = value.Next.December;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_February()
        {
            Date value = "2012-11-09";

            Date expected = "2013-02-09";
            var actual = value.Next.February;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Friday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-16";
            var actual = value.Next.Friday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_January()
        {
            Date value = "2012-11-09";

            Date expected = "2013-01-09";
            var actual = value.Next.January;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_July()
        {
            Date value = "2012-11-09";

            Date expected = "2013-07-09";
            var actual = value.Next.July;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_June()
        {
            Date value = "2012-11-09";

            Date expected = "2013-06-09";
            var actual = value.Next.June;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_March()
        {
            Date value = "2012-11-09";

            Date expected = "2013-03-09";
            var actual = value.Next.March;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_May()
        {
            Date value = "2012-11-09";

            Date expected = "2013-05-09";
            var actual = value.Next.May;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Monday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-12";
            var actual = value.Next.Monday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Month()
        {
            Date value = "2012-11-09";

            Date expected = "2012-12-09";
            var actual = value.Next.Month;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_November()
        {
            Date value = "2012-11-09";

            Date expected = "2013-11-09";
            var actual = value.Next.November;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_October()
        {
            Date value = "2012-11-09";

            Date expected = "2013-10-09";
            var actual = value.Next.October;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Saturday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-10";
            var actual = value.Next.Saturday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_September()
        {
            Date value = "2012-11-09";

            Date expected = "2013-09-09";
            var actual = value.Next.September;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Sunday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-11";
            var actual = value.Next.Sunday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Thursday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-15";
            var actual = value.Next.Thursday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Tuesday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-13";
            var actual = value.Next.Tuesday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Wednesday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-14";
            var actual = value.Next.Wednesday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Week()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-16";
            var actual = value.Next.Week;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Next_Year()
        {
            Date value = "2012-11-09";

            Date expected = "2013-11-09";
            var actual = value.Next.Year;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Between_Date()
        {
            var date = Date.Today.LocalTime;
            var other = date.AddWeeks(1);

            var expected = new DateTimePeriod
                               {
                                   Ending = other.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Between(other);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Between_DateEarlier()
        {
            var date = Date.Today.LocalTime;
            var other = date.AddWeeks(-1);

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = other.ToDateTime()
                               };
            var actual = date.Period.Between(other);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Between_DateSame()
        {
            var date = Date.Today.LocalTime;
            var other = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = other.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Between(other);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Days_int()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.AddDays(1).ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Days(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Days_intNegative()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.AddDays(-1).ToDateTime()
                               };
            var actual = date.Period.Days(-1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Days_intZero()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Days(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Months_int()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.AddMonths(1).AddDays(-1).ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Months(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Months_intNegative()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.AddMonths(-1).AddDays(1).ToDateTime()
                               };
            var actual = date.Period.Months(-1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Months_intZero()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Months(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Since_Date()
        {
            var date = Date.Today.LocalTime;
            var since = date.AddWeeks(-1);

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = since.ToDateTime()
                               };
            var actual = date.Period.Since(since);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Since_DateLater()
        {
            var date = Date.Today.LocalTime;
            var since = date.AddDays(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => date.Period.Since(since));
        }

        [Fact]
        public void prop_Period_Since_DateSame()
        {
            var date = Date.Today.LocalTime;
            var since = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = since.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Since(since);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Until_Date()
        {
            var date = Date.Today.LocalTime;
            var until = date.AddDays(1);

            var expected = new DateTimePeriod
                               {
                                   Ending = until.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Until(until);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Until_DateEarlier()
        {
            var date = Date.Today.LocalTime;
            var until = date.AddDays(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => date.Period.Until(until));
        }

        [Fact]
        public void prop_Period_Until_DateSame()
        {
            var date = Date.Today.LocalTime;
            var until = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = until.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Until(until);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Weeks_int()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.AddDays(13).ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Weeks(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Weeks_intNegative()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.AddDays(-13).ToDateTime()
                               };
            var actual = date.Period.Weeks(-2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Weeks_intZero()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Weeks(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Years_int()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.AddYears(1).AddDays(-1).ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Years(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Years_intNegative()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.AddYears(-1).AddDays(1).ToDateTime()
                               };
            var actual = date.Period.Years(-1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Period_Years_intZero()
        {
            var date = Date.Today.LocalTime;

            var expected = new DateTimePeriod
                               {
                                   Ending = date.ToDateTime(), 
                                   Beginning = date.ToDateTime()
                               };
            var actual = date.Period.Years(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Previous)
                            .TypeIs<IGetPreviousDate>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Previous_April()
        {
            Date value = "2012-11-09";

            Date expected = "2012-04-09";
            var actual = value.Previous.April;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_August()
        {
            Date value = "2012-11-09";

            Date expected = "2012-08-09";
            var actual = value.Previous.August;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Day()
        {
            Date value = "2012-08-24";

            Date expected = "2012-08-23";
            var actual = value.Previous.Day;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_December()
        {
            Date value = "2012-11-09";

            Date expected = "2011-12-09";
            var actual = value.Previous.December;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_February()
        {
            Date value = "2012-11-09";

            Date expected = "2012-02-09";
            var actual = value.Previous.February;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Friday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-02";
            var actual = value.Previous.Friday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_January()
        {
            Date value = "2012-11-09";

            Date expected = "2012-01-09";
            var actual = value.Previous.January;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_July()
        {
            Date value = "2012-11-09";

            Date expected = "2012-07-09";
            var actual = value.Previous.July;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_June()
        {
            Date value = "2012-11-09";

            Date expected = "2012-06-09";
            var actual = value.Previous.June;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_March()
        {
            Date value = "2012-11-09";

            Date expected = "2012-03-09";
            var actual = value.Previous.March;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_May()
        {
            Date value = "2012-11-09";

            Date expected = "2012-05-09";
            var actual = value.Previous.May;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Monday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-05";
            var actual = value.Previous.Monday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Month()
        {
            Date value = "2012-11-09";

            Date expected = "2012-10-09";
            var actual = value.Previous.Month;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_November()
        {
            Date value = "2012-11-09";

            Date expected = "2011-11-09";
            var actual = value.Previous.November;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_October()
        {
            Date value = "2012-11-09";

            Date expected = "2012-10-09";
            var actual = value.Previous.October;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Saturday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-03";
            var actual = value.Previous.Saturday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_September()
        {
            Date value = "2012-11-09";

            Date expected = "2012-09-09";
            var actual = value.Previous.September;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Sunday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-04";
            var actual = value.Previous.Sunday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Thursday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-08";
            var actual = value.Previous.Thursday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Tuesday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-06";
            var actual = value.Previous.Tuesday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Wednesday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-07";
            var actual = value.Previous.Wednesday;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Week()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-02";
            var actual = value.Previous.Week;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Previous_Year()
        {
            Date value = "2012-11-09";

            Date expected = "2011-11-09";
            var actual = value.Previous.Year;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Today_For_TimeZoneInfo()
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

            var expected = new Date(DateTime.Today.ToLocalTime(zone));
            var actual = Date.Today.For(zone);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Today_For_TimeZoneInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Date.Today.For(null));
        }

        [Fact]
        public void prop_Today_LocalTime()
        {
            var expected = new Date(DateTime.Today);
            var actual = Date.Today.LocalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Today_UniversalTime()
        {
            var expected = new Date(DateTime.Today.ToUniversalTime());
            var actual = Date.Today.UniversalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Tomorrow_For_TimeZoneInfo()
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

            var expected = new Date(DateTime.Today.AddDays(1).ToLocalTime(zone));
            var actual = Date.Tomorrow.For(zone);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Tomorrow_For_TimeZoneInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Date.Tomorrow.For(null));
        }

        [Fact]
        public void prop_Tomorrow_LocalTime()
        {
            var expected = new Date(DateTime.Today.AddDays(1));
            var actual = Date.Tomorrow.LocalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Tomorrow_UniversalTime()
        {
            var expected = new Date(DateTime.Today.AddDays(1).ToUniversalTime());
            var actual = Date.Tomorrow.UniversalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Year()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Year)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Yesterday_For_TimeZoneInfo()
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

            var expected = new Date(DateTime.Today.AddDays(-1).ToLocalTime(zone));
            var actual = Date.Yesterday.For(zone);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Yesterday_For_TimeZoneInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Date.Yesterday.For(null));
        }

        [Fact]
        public void prop_Yesterday_LocalTime()
        {
            var expected = new Date(DateTime.Today.AddDays(-1));
            var actual = Date.Yesterday.LocalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Yesterday_UniversalTime()
        {
            var expected = new Date(DateTime.Today.AddDays(-1).ToUniversalTime());
            var actual = Date.Yesterday.UniversalTime;

            Assert.Equal(expected, actual);
        }
    }
}