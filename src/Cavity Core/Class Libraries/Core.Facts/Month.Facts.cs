namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    using Xunit;
    using Xunit.Extensions;

    public sealed class MonthFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Month>().IsValueType()
                                                     .Implements<IEquatable<Month>>()
                                                     .Serializable()
                                                     .IsDecoratedWith<ImmutableObjectAttribute>()
                                                     .Implements<IComparable>()
                                                     .Implements<IComparable<Month>>()
                                                     .Implements<IEquatable<Month>>()
                                                     .Implements<IChangeMonth<Month>>()
                                                     .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Month());
        }

        [Fact]
        public void ctor_Date()
        {
            Assert.NotNull(new Month(Date.Today.LocalTime));
        }

        [Fact]
        public void ctor_DateTime()
        {
            Assert.NotNull(new Month(DateTime.Today));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Month(1999, 12);
            Month actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Month(1999, 12));
                stream.Position = 0;
                actual = (Month)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_int_MonthOfYear()
        {
            Assert.NotNull(new Month(1999, MonthOfYear.May));
        }

        [Fact]
        public void ctor_int_int()
        {
            Assert.NotNull(new Month(1999, 12));
        }

        [Fact]
        public void ctor_int_int0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Month(1999, 0));
        }

        [Fact]
        public void ctor_int_int13()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Month(1999, 13));
        }

        [Fact]
        public void opDecrement()
        {
            var expected = Month.Today.LocalTime.AddMonths(-1);
            var actual = Month.Today.LocalTime;
            actual--;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_Month_Month()
        {
            var obj = new Month();
            var comparand = new Month();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreaterThanOrEqual_Month_Month()
        {
            Month one = "1999-12";
            Month two = "2000-01";

            Assert.True(two >= one);
            Assert.True(two >= "2000-01");
        }

        [Fact]
        public void opGreaterThan_Month_Month()
        {
            Month one = "1999-12";
            Month two = "2000-01";

            Assert.True(two > one);
        }

        [Fact]
        public void opImplicit_Month_string()
        {
            var expected = new Month(1999, 12);
            Month actual = "1999-12";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Month()
        {
            const string expected = "1999-12";
            string actual = new Month(1999, 12);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIncrement()
        {
            var expected = Month.Today.LocalTime.AddMonths(1);
            var actual = Month.Today.LocalTime;
            actual++;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Month_Month()
        {
            var obj = new Month();
            var comparand = new Month();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLessThan_Month_Month()
        {
            Month one = "1999-12";
            Month two = "2000-01";

            Assert.True(one < two);
        }

        [Fact]
        public void opLesserThanOrEqual_Month_Month()
        {
            Month one = "1999-12";
            Month two = "2000-01";

            Assert.True(one <= two);
            Assert.True(two <= "2000-01");
        }

        [Theory]
        [InlineData("2012-04", "2012-03", 1)]
        [InlineData("2012-03", "2012-03", 0)]
        [InlineData("2012-02", "2012-03", -1)]
        public void op_AddMonths_int(string expected, 
                                     string month, 
                                     int value)
        {
            var actual = ((Month)month).AddMonths(value);

            Assert.Equal((Month)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddMonths_int_whenArgumentOutOfRangeException(int value)
        {
            var month = Month.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => month.AddMonths(value));
        }

        [Theory]
        [InlineData("2012-06", "2012-03", 1)]
        [InlineData("2012-03", "2012-03", 0)]
        [InlineData("2011-12", "2012-03", -1)]
        public void op_AddQuarters_int(string expected, 
                                       string month, 
                                       int value)
        {
            var actual = ((Month)month).AddQuarters(value);

            Assert.Equal((Month)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddQuarters_int_whenArgumentOutOfRangeException(int value)
        {
            var month = Month.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => month.AddQuarters(value));
        }

        [Theory]
        [InlineData("2013-03", "2012-03", 1)]
        [InlineData("2012-03", "2012-03", 0)]
        [InlineData("2011-03", "2012-03", -1)]
        public void op_AddYears_int(string expected, 
                                    string month, 
                                    int value)
        {
            var actual = ((Month)month).AddYears(value);

            Assert.Equal((Month)expected, actual);
        }

        [Fact]
        public void op_Change_Month_int()
        {
            Month value = "2012-11-09";

            Month expected = "2012-06-09";
            var actual = value.Change.Month(6);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Change_To_MonthOfYear()
        {
            Month value = "2012-11-09";

            Month expected = "2012-05-09";
            var actual = value.Change.To(MonthOfYear.May);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Change_Year_int()
        {
            Month value = "2012-11-09";

            Month expected = "1999-11-09";
            var actual = value.Change.Year(1999);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_Month()
        {
            Month one = "1999-12";
            Month two = "2000-01";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            Month one = "1999-12";
            object two = (Month)"2000-01";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Month().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Month().CompareTo(null));
        }

        [Fact]
        public void op_Compare_Month_Month()
        {
            var expected = DateTime.Compare(new DateTime(1999, 12, 31), new DateTime(2000, 1, 1));
            var actual = Month.Compare("1999-12", "2000-01");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Decrement()
        {
            var expected = Month.Today.LocalTime.AddMonths(-1);
            var actual = Month.Today.LocalTime.Decrement();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_Month()
        {
            var obj = new Month();

            Assert.True(new Month().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Month();

            Assert.True(new Month().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Month(DateTime.Now);

            Assert.False(new Month().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.Throws<InvalidCastException>(() => new Month().Equals(obj));

            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Month().Equals(null as object));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new Month(1999, 12);
            var actual = Month.FromString("1999-12");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Month.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Month.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = Date.Today.LocalTime.FirstOfMonth.GetHashCode();
            var actual = new Month(DateTime.Now).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Month(1999, 12);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Month), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "12/01/1999 00:00:00";

            ISerializable value = new Month(1999, 12);

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Increment()
        {
            var expected = Month.Today.LocalTime.AddMonths(1);
            var actual = Month.Today.LocalTime.Increment();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2012-04", "2012-03", MonthOfYear.April)]
        [InlineData("2012-05", "2012-03", MonthOfYear.May)]
        [InlineData("2012-06", "2012-03", MonthOfYear.June)]
        [InlineData("2012-07", "2012-03", MonthOfYear.July)]
        [InlineData("2012-08", "2012-03", MonthOfYear.August)]
        [InlineData("2012-09", "2012-03", MonthOfYear.September)]
        [InlineData("2012-10", "2012-03", MonthOfYear.October)]
        [InlineData("2012-11", "2012-03", MonthOfYear.November)]
        [InlineData("2012-12", "2012-03", MonthOfYear.December)]
        [InlineData("2013-01", "2012-03", MonthOfYear.January)]
        [InlineData("2013-02", "2012-03", MonthOfYear.February)]
        [InlineData("2013-03", "2012-03", MonthOfYear.March)]
        public void op_Next_MonthOfYear(string expected, 
                                        string month, 
                                        MonthOfYear value)
        {
            Month day = month;
            var actual = day.Next(value);

            Assert.Equal((Month)expected, actual);
            Assert.Equal((Month)month, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_Next_MonthOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Month.Today.LocalTime.Next((MonthOfYear)value));
        }

        [Theory]
        [InlineData("2011-03", "2012-03", MonthOfYear.March)]
        [InlineData("2011-04", "2012-03", MonthOfYear.April)]
        [InlineData("2011-05", "2012-03", MonthOfYear.May)]
        [InlineData("2011-06", "2012-03", MonthOfYear.June)]
        [InlineData("2011-07", "2012-03", MonthOfYear.July)]
        [InlineData("2011-08", "2012-03", MonthOfYear.August)]
        [InlineData("2011-09", "2012-03", MonthOfYear.September)]
        [InlineData("2011-10", "2012-03", MonthOfYear.October)]
        [InlineData("2011-11", "2012-03", MonthOfYear.November)]
        [InlineData("2011-12", "2012-03", MonthOfYear.December)]
        [InlineData("2012-01", "2012-03", MonthOfYear.January)]
        [InlineData("2012-02", "2012-03", MonthOfYear.February)]
        public void op_Previous_MonthOfYear(string expected, 
                                            string month, 
                                            MonthOfYear value)
        {
            Month day = month;
            var actual = day.Previous(value);

            Assert.Equal((Month)expected, actual);
            Assert.Equal((Month)month, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_Previous_MonthOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Month.Today.LocalTime.Previous((MonthOfYear)value));
        }

        [Fact]
        public void op_ToDate()
        {
            var expected = Date.MinValue;
            var actual = new Month().ToDate();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime()
        {
            var expected = DateTime.MinValue.Date;
            var actual = new Month().ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime_whenValue()
        {
            var expected = Date.Today.LocalTime.FirstOfMonth.ToDateTime();
            var actual = new Month(DateTime.Now).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "0001-01";
            var actual = new Month().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenValue()
        {
            const string expected = "1999-12";
            var actual = new Month(1999, 12).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Change()
        {
            Assert.True(new PropertyExpectations<Month>(x => x.Change)
                            .TypeIs<IChangeMonth<Month>>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_DaysInMonth()
        {
            Assert.True(new PropertyExpectations<Month>(x => x.DaysInMonth)
                            .TypeIs<int>()
                            .DefaultValueIs(31)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_IsLeapYear()
        {
            Assert.True(new PropertyExpectations<Month>(x => x.IsLeapYear)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_MaxValue()
        {
            var expected = new Month(DateTime.MaxValue);
            var actual = Month.MaxValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MinValue()
        {
            var expected = new Month(DateTime.MinValue);
            var actual = Month.MinValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MonthOfYear()
        {
            Assert.True(new PropertyExpectations<Month>(x => x.MonthOfYear)
                            .TypeIs<MonthOfYear>()
                            .DefaultValueIs(MonthOfYear.January)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Today_LocalTime()
        {
            var expected = new Month(DateTime.Today);
            var actual = Month.Today.LocalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Today_UniversalTime()
        {
            var expected = new Month(DateTime.Today.ToUniversalTime());
            var actual = Month.Today.UniversalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Year()
        {
            Assert.True(new PropertyExpectations<Month>(x => x.Year)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }
    }
}