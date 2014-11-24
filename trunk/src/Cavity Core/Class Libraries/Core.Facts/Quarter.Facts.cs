namespace Cavity
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;
    using Xunit.Extensions;

    public sealed class QuarterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Quarter>().IsValueType()
                                                       .Implements<IEquatable<Quarter>>()
                                                       .Serializable()
                                                       .IsDecoratedWith<ImmutableObjectAttribute>()
                                                       .Implements<IComparable>()
                                                       .Implements<IComparable<Quarter>>()
                                                       .Implements<IEquatable<Quarter>>()
                                                       .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Quarter());
        }

        [Fact]
        public void ctor_Date()
        {
            Assert.NotNull(new Quarter(Date.Today.LocalTime));
        }

        [Fact]
        public void ctor_DateTime()
        {
            Assert.NotNull(new Quarter(DateTime.Today));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Quarter(1999, 3);
            Quarter actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Quarter(1999, 3));
                stream.Position = 0;
                actual = (Quarter)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_int_QuarterOfYear()
        {
            Assert.NotNull(new Quarter(1999, QuarterOfYear.Q2));
        }

        [Fact]
        public void ctor_int_int()
        {
            Assert.NotNull(new Quarter(1999, 2));
        }

        [Fact]
        public void ctor_int_int0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Quarter(1999, 0));
        }

        [Fact]
        public void ctor_int_int13()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Quarter(1999, 13));
        }

        [Fact]
        public void opDecrement()
        {
            var expected = Quarter.Today.LocalTime.AddQuarters(-1);
            var actual = Quarter.Today.LocalTime;
            actual--;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_Quarter_Quarter()
        {
            var obj = new Quarter();
            var comparand = new Quarter();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreaterThanOrEqual_Quarter_Quarter()
        {
            Quarter one = "1999 Q4";
            Quarter two = "2000 Q1";

            Assert.True(two >= one);
            Assert.True(two >= "2000 Q1");
        }

        [Fact]
        public void opGreaterThan_Quarter_Quarter()
        {
            Quarter one = "1999 Q4";
            Quarter two = "2000 Q1";

            Assert.True(two > one);
        }

        [Fact]
        public void opImplicit_Quarter_string()
        {
            var expected = new Quarter(1999, 4);
            Quarter actual = "1999 Q4";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Quarter()
        {
            const string expected = "1999 Q4";
            string actual = new Quarter(1999, 4);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIncrement()
        {
            var expected = Quarter.Today.LocalTime.AddQuarters(1);
            var actual = Quarter.Today.LocalTime;
            actual++;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Quarter_Quarter()
        {
            var obj = new Quarter();
            var comparand = new Quarter();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLessThan_Quarter_Quarter()
        {
            Quarter one = "1999 Q4";
            Quarter two = "2000 Q1";

            Assert.True(one < two);
        }

        [Fact]
        public void opLesserThanOrEqual_Quarter_Quarter()
        {
            Quarter one = "1999 Q4";
            Quarter two = "2000 Q1";

            Assert.True(one <= two);
            Assert.True(two <= "2000 Q1");
        }

        [Theory]
        [InlineData("2012 Q4", "2012 Q3", 1)]
        [InlineData("2012 Q3", "2012 Q3", 0)]
        [InlineData("2012 Q2", "2012 Q3", -1)]
        public void op_AddQuarters_int(string expected,
                                       string quarter,
                                       int value)
        {
            var actual = ((Quarter)quarter).AddQuarters(value);

            Assert.Equal((Quarter)expected, actual);
        }

        [Theory]
        [InlineData(2147483647)]
        [InlineData(-2147483648)]
        public void op_AddQuarters_int_whenArgumentOutOfRangeException(int value)
        {
            var quarter = Quarter.Today.LocalTime;

            Assert.Throws<ArgumentOutOfRangeException>(() => quarter.AddQuarters(value));
        }

        [Theory]
        [InlineData("2013 Q3", "2012 Q3", 1)]
        [InlineData("2012 Q3", "2012 Q3", 0)]
        [InlineData("2011 Q3", "2012 Q3", -1)]
        public void op_AddYears_int(string expected,
                                    string quarter,
                                    int value)
        {
            var actual = ((Quarter)quarter).AddYears(value);

            Assert.Equal((Quarter)expected, actual);
        }

        ////[Fact]
        ////public void op_Change_Quarter_int()
        ////{
        ////    Quarter value = "2012-11-09";

        ////    Quarter expected = "2012-06-09";
        ////    var actual = value.Change.Quarter(6);

        ////    Assert.Equal(expected, actual);
        ////}

        ////[Fact]
        ////public void op_Change_To_QuarterOfYear()
        ////{
        ////    Quarter value = "2012-11-09";

        ////    Quarter expected = "2012-05-09";
        ////    var actual = value.Change.To(QuarterOfYear.May);

        ////    Assert.Equal(expected, actual);
        ////}

        ////[Fact]
        ////public void op_Change_Year_int()
        ////{
        ////    Quarter value = "2012-11-09";

        ////    Quarter expected = "1999-11-09";
        ////    var actual = value.Change.Year(1999);

        ////    Assert.Equal(expected, actual);
        ////}

        [Fact]
        public void op_CompareTo_Quarter()
        {
            Quarter one = "1999 Q4";
            Quarter two = "2000 Q1";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            Quarter one = "1999 Q4";
            object two = (Quarter)"2000 Q1";

            const long expected = -1;
            var actual = one.CompareTo(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Quarter().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Quarter().CompareTo(null));
        }

        [Fact]
        public void op_Compare_Quarter_Quarter()
        {
            var expected = DateTime.Compare(new DateTime(1999, 12, 31), new DateTime(2000, 1, 1));
            var actual = Quarter.Compare("1999 Q4", "2000 Q1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Decrement()
        {
            var expected = Quarter.Today.LocalTime.AddQuarters(-1);
            var actual = Quarter.Today.LocalTime.Decrement();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_Quarter()
        {
            var obj = new Quarter();

            Assert.True(new Quarter().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Quarter();

            Assert.True(new Quarter().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Quarter(DateTime.Now);

            Assert.False(new Quarter().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.Throws<InvalidCastException>(() => new Quarter().Equals(obj));

            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Quarter().Equals(null as object));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new Quarter(1999, 4);
            var actual = Quarter.FromString("1999 Q4");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Quarter.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Quarter.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = new DateTime(2012, 1, 1).GetHashCode();
            var actual = new Quarter(2012, 1).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Quarter(1999, 3);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Quarter), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "10/01/1999 00:00:00";

            ISerializable value = new Quarter(1999, 4);

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Increment()
        {
            var expected = Quarter.Today.LocalTime.AddQuarters(1);
            var actual = Quarter.Today.LocalTime.Increment();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2013 Q1", "2012 Q3", QuarterOfYear.Q1)]
        [InlineData("2013 Q2", "2012 Q3", QuarterOfYear.Q2)]
        [InlineData("2013 Q3", "2012 Q3", QuarterOfYear.Q3)]
        [InlineData("2012 Q4", "2012 Q3", QuarterOfYear.Q4)]
        public void op_Next_QuarterOfYear(string expected,
                                          string quarter,
                                          QuarterOfYear value)
        {
            Quarter day = quarter;
            var actual = day.Next(value);

            Assert.Equal((Quarter)expected, actual);
            Assert.Equal((Quarter)quarter, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_Next_QuarterOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Quarter.Today.LocalTime.Next((QuarterOfYear)value));
        }

        [Theory]
        [InlineData("2012 Q1", "2012 Q3", QuarterOfYear.Q1)]
        [InlineData("2012 Q2", "2012 Q3", QuarterOfYear.Q2)]
        [InlineData("2011 Q3", "2012 Q3", QuarterOfYear.Q3)]
        [InlineData("2011 Q4", "2012 Q3", QuarterOfYear.Q4)]
        public void op_Previous_QuarterOfYear(string expected,
                                              string quarter,
                                              QuarterOfYear value)
        {
            Quarter day = quarter;
            var actual = day.Previous(value);

            Assert.Equal((Quarter)expected, actual);
            Assert.Equal((Quarter)quarter, day);
        }

        [Theory]
        [InlineData(999)]
        public void op_Previous_QuarterOfYear_whenArgumentOutOfRangeException(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Quarter.Today.LocalTime.Previous((QuarterOfYear)value));
        }

        [Fact]
        public void op_ToDate()
        {
            var expected = Date.MinValue;
            var actual = new Quarter().ToDate();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime()
        {
            var expected = DateTime.MinValue.Date;
            var actual = new Quarter().ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime_whenValue()
        {
            var expected = new Date(2012, 4, 1).ToDateTime();
            var actual = new Quarter(2012, 2).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "0001 Q1";
            var actual = new Quarter().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenValue()
        {
            const string expected = "1999 Q4";
            var actual = new Quarter(1999, 4).ToString();

            Assert.Equal(expected, actual);
        }

        ////[Fact]
        ////public void prop_Change()
        ////{
        ////    Assert.True(new PropertyExpectations<Quarter>(x => x.Change)
        ////                    .TypeIs<IChangeQuarter<Quarter>>()
        ////                    .DefaultValueIsNotNull()
        ////                    .IsNotDecorated()
        ////                    .Result);
        ////}

        ////[Fact]
        ////public void prop_DaysInQuarter()
        ////{
        ////    Assert.True(new PropertyExpectations<Quarter>(x => x.DaysInQuarter)
        ////                    .TypeIs<int>()
        ////                    .DefaultValueIs(31)
        ////                    .IsNotDecorated()
        ////                    .Result);
        ////}

        ////[Fact]
        ////public void prop_IsLeapYear()
        ////{
        ////    Assert.True(new PropertyExpectations<Quarter>(x => x.IsLeapYear)
        ////                    .TypeIs<bool>()
        ////                    .DefaultValueIs(false)
        ////                    .IsNotDecorated()
        ////                    .Result);
        ////}

        [Fact]
        public void prop_MaxValue()
        {
            var expected = new Quarter(DateTime.MaxValue);
            var actual = Quarter.MaxValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MinValue()
        {
            var expected = new Quarter(DateTime.MinValue);
            var actual = Quarter.MinValue;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_QuarterOfYear()
        {
            Assert.True(new PropertyExpectations<Quarter>(x => x.QuarterOfYear)
                            .TypeIs<QuarterOfYear>()
                            .DefaultValueIs(QuarterOfYear.Q1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Today_LocalTime()
        {
            var expected = new Quarter(DateTime.Today);
            var actual = Quarter.Today.LocalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Today_UniversalTime()
        {
            var expected = new Quarter(DateTime.Today.ToUniversalTime());
            var actual = Quarter.Today.UniversalTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Year()
        {
            Assert.True(new PropertyExpectations<Quarter>(x => x.Year)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }
    }
}