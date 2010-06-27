namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpDateFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpDate>()
                .IsValueType()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpDate());
        }

        [Fact]
        public void ctor_DateTime()
        {
            Assert.NotNull(new HttpDate(DateTime.UtcNow));
        }

        [Fact]
        public void opImplicit_DateTime_HttpDate()
        {
            DateTime expected = new DateTime(1994, 11, 15, 8, 12, 31);
            DateTime actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal<DateTime>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpDate_DateTime()
        {
            HttpDate expected = new DateTime(1994, 11, 15, 8, 12, 31);
            HttpDate actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal<HttpDate>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpDate()
        {
            string expected = "Tue, 15 Nov 1994 08:12:31 GMT";
            string actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpDate_stringNull()
        {
            HttpDate obj;

            Assert.Throws<ArgumentNullException>(() => obj = null as string);
        }

        [Fact]
        public void opImplicit_HttpDate_stringEmpty()
        {
            HttpDate obj;

            Assert.Throws<FormatException>(() => obj = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpDate_string()
        {
            HttpDate expected = "Tue, 15 Nov 1994 08:12:31 GMT";
            HttpDate actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal<HttpDate>(expected, actual);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenTrue()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenFalse()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenSame()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenTrue()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenFalse()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenSame()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenSame()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenTrue()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenFalse()
        {
            HttpDate operand1 = new HttpDate(new DateTime(2009, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenSame()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenTrue()
        {
            HttpDate operand1 = new HttpDate(new DateTime(2009, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenFalse()
        {
            HttpDate operand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpDate_HttpDate_whenEqual()
        {
            HttpDate comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate comparand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal<int>(0, HttpDate.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpDate_HttpDate_whenSame()
        {
            HttpDate comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate comparand2 = comparand1;

            Assert.Equal<int>(0, HttpDate.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpDateGreater_HttpDate()
        {
            HttpDate comparand1 = new HttpDate(new DateTime(2009, 12, 31));
            HttpDate comparand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(HttpDate.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpDateLesser_HttpDate()
        {
            HttpDate comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate comparand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(HttpDate.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_FromString_string()
        {
            HttpDate expected = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));
            HttpDate actual = HttpDate.FromString("Tue, 15 Nov 1994 08:12:31 GMT");

            Assert.Equal<HttpDate>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpDate.FromString(null as string));
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<FormatException>(() => HttpDate.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringInvalid()
        {
            Assert.Throws<FormatException>(() => HttpDate.FromString("not a date"));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.True(new HttpDate().CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            HttpDate value = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal<int>(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            HttpDate left = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate right = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_object_whenLesser()
        {
            HttpDate left = new HttpDate(new DateTime(1999, 12, 31));
            HttpDate right = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_object_whenGreater()
        {
            HttpDate left = new HttpDate(new DateTime(2009, 12, 31));
            HttpDate right = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpDate(new DateTime(1999, 12, 31)).CompareTo(123));
        }

        [Fact]
        public void op_Equals_object()
        {
            HttpDate value = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(new HttpDate(new DateTime(1999, 12, 31)).Equals(value));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            HttpDate value = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(value.Equals(value));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new HttpDate().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new HttpDate(new DateTime(1999, 12, 31)).Equals("123"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            HttpDate value = new HttpDate(new DateTime(1999, 12, 31));

            int expected = value.ToString().GetHashCode();
            int actual = value.GetHashCode();

            Assert.Equal<int>(expected, actual);
        }

        [Fact]
        public void op_ToDateTime()
        {
            DateTime expected = new DateTime(1994, 11, 15, 8, 12, 31);
            DateTime actual = new HttpDate(expected).ToDateTime();

            Assert.Equal<DateTime>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "Tue, 15 Nov 1994 08:12:31 GMT";
            string actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31)).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}