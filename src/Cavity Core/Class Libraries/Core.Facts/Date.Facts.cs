namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DateFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Date>()
                            .IsValueType()
                            .Implements<IEquatable<Date>>()
                            .Serializable()
                            .IsDecoratedWith<ImmutableObjectAttribute>()
                            .Implements<IComparable>()
                            .Implements<IComparable<Date>>()
                            .Implements<IGetNextDate>()
                            .Implements<IGetPreviousDate>()
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
            var actual = (Date)new Date(new DateTime(2012, 03, 25, 2, 1, 0, DateTimeKind.Local)).ToDateTime().AddDays(1);

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
        public void opImplicit_DateTime_Date()
        {
            var expected = DateTime.Today;
            DateTime actual = new Date(DateTime.Now);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Date_DateTime()
        {
            var expected = new Date(DateTime.Now);
            Date actual = DateTime.Now;

            Assert.Equal(expected, actual);
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
            var date = Date.Today;

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
            var date = Date.Today;

            Assert.Throws<ArgumentOutOfRangeException>(() => date.AddQuarters(value));
        }

        [Theory]
        [InlineData("2012-04-02", "2012-03-26", 1)]
        [InlineData("2012-03-26", "2012-03-26", 0)]
        [InlineData("2012-03-19", "2012-03-26", -1)]
        public void op_AddWeeks_int(string expected, 
                                    string date, 
                                    int value)
        {
            var actual = ((Date)date).AddWeeks(value);

            Assert.Equal((Date)expected, actual);
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
        public void op_CompareTo_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
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
            Assert.Throws<ArgumentNullException>(() => new Date().CompareTo(null));
        }

        [Fact]
        public void op_Compare_Date_Date()
        {
            var expected = DateTime.Compare(new DateTime(1999, 12, 31), new DateTime(2000, 1, 1));
            var actual = Date.Compare("1999-12-31", "2000-01-01");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_Date()
        {
            var obj = new Date();

            Assert.True(new Date().Equals(obj));
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
        public void op_Next_Month()
        {
            Date value = "2012-11-09";

            Date expected = "2012-12-09";
            var actual = value.Next.Month();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Next_Month_int()
        {
            Date value = "2012-11-09";

            Date expected = "2012-12-25";
            var actual = value.Next.Month(25);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Next_Year()
        {
            Date value = "2012-11-09";

            Date expected = "2013-11-09";
            var actual = value.Next.Year();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Next_Year_MonthOfYear_int()
        {
            Date value = "2012-11-09";

            Date expected = "2013-06-30";
            var actual = value.Next.Year(MonthOfYear.June, 30);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Next_Year_int_int()
        {
            Date value = "2012-11-09";

            Date expected = "2013-06-30";
            var actual = value.Next.Year(6, 30);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Previous_Month()
        {
            Date value = "2012-11-09";

            Date expected = "2012-10-09";
            var actual = value.Previous.Month();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Previous_Month_int()
        {
            Date value = "2012-11-09";

            Date expected = "2012-10-25";
            var actual = value.Previous.Month(25);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Previous_Year()
        {
            Date value = "2012-11-09";

            Date expected = "2011-11-09";
            var actual = value.Previous.Year();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Previous_Year_MonthOfYear_int()
        {
            Date value = "2012-11-09";

            Date expected = "2011-06-30";
            var actual = value.Previous.Year(MonthOfYear.June, 30);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Previous_Year_int_int()
        {
            Date value = "2012-11-09";

            Date expected = "2011-06-30";
            var actual = value.Previous.Year(6, 30);

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

        [Theory]
        [InlineData("2012-03-27", "2012-03-26", DayOfWeek.Tuesday)]
        [InlineData("2012-03-28", "2012-03-26", DayOfWeek.Wednesday)]
        [InlineData("2012-03-29", "2012-03-26", DayOfWeek.Thursday)]
        [InlineData("2012-03-30", "2012-03-26", DayOfWeek.Friday)]
        [InlineData("2012-03-31", "2012-03-26", DayOfWeek.Saturday)]
        [InlineData("2012-04-01", "2012-03-26", DayOfWeek.Sunday)]
        [InlineData("2012-04-02", "2012-03-26", DayOfWeek.Monday)]
        public void op_ToNext_DayOfWeek(string expected, 
                                        string date, 
                                        DayOfWeek value)
        {
            Date day = date;
            var actual = day.ToNext(value);

            Assert.Equal((Date)expected, actual);
            Assert.Equal((Date)date, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_ToNext_DayOfWeek_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.Today.ToNext((DayOfWeek)value));
        }

        [Theory]
        [InlineData("2012-04-26", "2012-03-26", MonthOfYear.April)]
        [InlineData("2012-05-26", "2012-03-26", MonthOfYear.May)]
        [InlineData("2012-06-26", "2012-03-26", MonthOfYear.June)]
        [InlineData("2012-07-26", "2012-03-26", MonthOfYear.July)]
        [InlineData("2012-08-26", "2012-03-26", MonthOfYear.August)]
        [InlineData("2012-09-26", "2012-03-26", MonthOfYear.September)]
        [InlineData("2012-10-26", "2012-03-26", MonthOfYear.October)]
        [InlineData("2012-11-26", "2012-03-26", MonthOfYear.November)]
        [InlineData("2012-12-26", "2012-03-26", MonthOfYear.December)]
        [InlineData("2013-01-26", "2012-03-26", MonthOfYear.January)]
        [InlineData("2013-02-26", "2012-03-26", MonthOfYear.February)]
        [InlineData("2013-03-26", "2012-03-26", MonthOfYear.March)]
        public void op_ToNext_MonthOfYear(string expected, 
                                          string date, 
                                          MonthOfYear value)
        {
            Date day = date;
            var actual = day.ToNext(value);

            Assert.Equal((Date)expected, actual);
            Assert.Equal((Date)date, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_ToNext_MonthOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.Today.ToNext((MonthOfYear)value));
        }

        [Theory]
        [InlineData("2012-03-19", "2012-03-26", DayOfWeek.Monday)]
        [InlineData("2012-03-20", "2012-03-26", DayOfWeek.Tuesday)]
        [InlineData("2012-03-21", "2012-03-26", DayOfWeek.Wednesday)]
        [InlineData("2012-03-22", "2012-03-26", DayOfWeek.Thursday)]
        [InlineData("2012-03-23", "2012-03-26", DayOfWeek.Friday)]
        [InlineData("2012-03-24", "2012-03-26", DayOfWeek.Saturday)]
        [InlineData("2012-03-25", "2012-03-26", DayOfWeek.Sunday)]
        public void op_ToPrevious_DayOfWeek(string expected, 
                                            string date, 
                                            DayOfWeek value)
        {
            Date day = date;
            var actual = day.ToPrevious(value);

            Assert.Equal((Date)expected, actual);
            Assert.Equal((Date)date, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_ToPrevious_DayOfWeek_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.Today.ToPrevious((DayOfWeek)value));
        }

        [Theory]
        [InlineData("2011-03-26", "2012-03-26", MonthOfYear.March)]
        [InlineData("2011-04-26", "2012-03-26", MonthOfYear.April)]
        [InlineData("2011-05-26", "2012-03-26", MonthOfYear.May)]
        [InlineData("2011-06-26", "2012-03-26", MonthOfYear.June)]
        [InlineData("2011-07-26", "2012-03-26", MonthOfYear.July)]
        [InlineData("2011-08-26", "2012-03-26", MonthOfYear.August)]
        [InlineData("2011-09-26", "2012-03-26", MonthOfYear.September)]
        [InlineData("2011-10-26", "2012-03-26", MonthOfYear.October)]
        [InlineData("2011-11-26", "2012-03-26", MonthOfYear.November)]
        [InlineData("2011-12-26", "2012-03-26", MonthOfYear.December)]
        [InlineData("2012-01-26", "2012-03-26", MonthOfYear.January)]
        [InlineData("2012-02-26", "2012-03-26", MonthOfYear.February)]
        public void op_ToPrevious_MonthOfYear(string expected, 
                                              string date, 
                                              MonthOfYear value)
        {
            Date day = date;
            var actual = day.ToPrevious(value);

            Assert.Equal((Date)expected, actual);
            Assert.Equal((Date)date, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_ToPrevious_MonthOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.Today.ToPrevious((MonthOfYear)value));
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
            Date expected = DateTime.MaxValue;
            var actual = Date.MaxValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MinValue()
        {
            Date expected = DateTime.MinValue;
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
        public void prop_Next_Day()
        {
            Date value = "2012-08-24";

            Date expected = "2012-08-25";
            var actual = value.Next.Day;

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
        public void prop_Next_Monday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-12";
            var actual = value.Next.Monday;

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
        public void prop_Previous()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Previous)
                            .TypeIs<IGetPreviousDate>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
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
        public void prop_Previous_Friday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-02";
            var actual = value.Previous.Friday;

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
        public void prop_Previous_Saturday()
        {
            Date value = "2012-11-09";

            Date expected = "2012-11-03";
            var actual = value.Previous.Saturday;

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
        public void prop_Today_get()
        {
            var expected = new Date(DateTime.Today);
            var actual = Date.Today;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Tomorrow_get()
        {
            var expected = new Date(DateTime.Today.AddDays(1));
            var actual = Date.Tomorrow;

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
        public void prop_Yesterday_get()
        {
            var expected = new Date(DateTime.Today.AddDays(-1));
            var actual = Date.Yesterday;

            Assert.Equal(expected, actual);
        }
    }
}