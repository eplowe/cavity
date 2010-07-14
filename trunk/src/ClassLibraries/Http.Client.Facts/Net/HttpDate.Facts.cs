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
            var expected = new DateTime(1994, 11, 15, 8, 12, 31);
            DateTime actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpDate_DateTime()
        {
            var expected = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));
            HttpDate actual = new DateTime(1994, 11, 15, 8, 12, 31);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpDate()
        {
            const string expected = "Tue, 15 Nov 1994 08:12:31 GMT";
            string actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));

            Assert.Equal(expected, actual);
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
            var expected = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));
            HttpDate actual = "Tue, 15 Nov 1994 08:12:31 GMT";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenTrue()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenFalse()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_HttpDate_HttpDate_whenSame()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenTrue()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenFalse()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_HttpDate_HttpDate_whenSame()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenSame()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenTrue()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_HttpDate_HttpDate_whenFalse()
        {
            var operand1 = new HttpDate(new DateTime(2009, 12, 31));
            var operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenSame()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenTrue()
        {
            var operand1 = new HttpDate(new DateTime(2009, 12, 31));
            var operand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_HttpDate_HttpDate_whenFalse()
        {
            var operand1 = new HttpDate(new DateTime(1999, 12, 31));
            var operand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_HttpDate_HttpDate_whenEqual()
        {
            var comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            var comparand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal(0, HttpDate.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpDate_HttpDate_whenSame()
        {
            var comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            var comparand2 = comparand1;

            Assert.Equal(0, HttpDate.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_HttpDateGreater_HttpDate()
        {
            var comparand1 = new HttpDate(new DateTime(2009, 12, 31));
            var comparand2 = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(HttpDate.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_HttpDateLesser_HttpDate()
        {
            var comparand1 = new HttpDate(new DateTime(1999, 12, 31));
            var comparand2 = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(HttpDate.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31));
            var actual = HttpDate.FromString("Tue, 15 Nov 1994 08:12:31 GMT");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpDate.FromString(null));
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
            var value = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal(0, value.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            var left = new HttpDate(new DateTime(1999, 12, 31));
            var right = new HttpDate(new DateTime(1999, 12, 31));

            Assert.Equal(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_object_whenLesser()
        {
            var left = new HttpDate(new DateTime(1999, 12, 31));
            var right = new HttpDate(new DateTime(2009, 12, 31));

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_object_whenGreater()
        {
            var left = new HttpDate(new DateTime(2009, 12, 31));
            var right = new HttpDate(new DateTime(1999, 12, 31));

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
            var value = new HttpDate(new DateTime(1999, 12, 31));

            Assert.True(new HttpDate(new DateTime(1999, 12, 31)).Equals(value));
        }

        [Fact]
        public void op_Equals_object_this()
        {
            var value = new HttpDate(new DateTime(1999, 12, 31));

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
            var value = new HttpDate(new DateTime(1999, 12, 31));

            var expected = value.ToString().GetHashCode();
            var actual = value.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime()
        {
            var expected = new DateTime(1994, 11, 15, 8, 12, 31);
            var actual = new HttpDate(expected).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "Tue, 15 Nov 1994 08:12:31 GMT";
            var actual = new HttpDate(new DateTime(1994, 11, 15, 8, 12, 31)).ToString();

            Assert.Equal(expected, actual);
        }
    }
}