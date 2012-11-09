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
        public void opGreater_Date_Date()
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
        public void opLesser_Date_Date()
        {
            Date one = "1999-12-31";
            Date two = "2000-01-01";

            Assert.True(one < two);
        }

        [Fact]
        public void op_Compare_Date_Date()
        {
            var expected = DateTime.Compare(new DateTime(1999, 12, 31), new DateTime(2000, 1, 1));
            var actual = Date.Compare("1999-12-31", "2000-01-01");

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

            Assert.Throws<InvalidCastException>(() => new Date().Equals(obj));
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
        public void prop_Month()
        {
            Assert.True(new PropertyExpectations<Date>(x => x.Month)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
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