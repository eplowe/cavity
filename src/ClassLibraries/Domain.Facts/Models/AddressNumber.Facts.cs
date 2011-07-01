namespace Cavity.Models
{
    using System;
    using Xunit;

    public sealed class AddressNumberFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AddressNumber>()
                            .IsValueType()
                            .IsNotDecorated()
                            .Implements<IComparable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new AddressNumber());
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new AddressNumber("123", "A"));
        }

        [Fact]
        public void opEquality_AddressNumber_AddressNumber_whenFalse()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("456", "A");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_AddressNumber_AddressNumber_whenSame()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_AddressNumber_AddressNumber_whenTrue()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("123", "A");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opGreater_AddressNumber_AddressNumber_whenFalse()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("456", "A");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_AddressNumber_AddressNumber_whenSame()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_AddressNumber_AddressNumber_whenTrue()
        {
            var operand1 = new AddressNumber("456", "A");
            var operand2 = new AddressNumber("123", "A");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opImplicit_AddressNumber_string()
        {
            AddressNumber expected = "123A";
            var actual = new AddressNumber("123", "A");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AddressNumber_stringEmpty()
        {
            var expected = new AddressNumber();
            AddressNumber actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AddressNumber_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (AddressNumber)(null as string));
        }

        [Fact]
        public void opImplicit_string_AddressNumber()
        {
            const string expected = "123A";
            string actual = new AddressNumber("123", "A");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_AddressNumber_AddressNumber_whenFalse()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("123", "A");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_AddressNumber_AddressNumber_whenSame()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_AddressNumber_AddressNumber_whenTrue()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("456", "A");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_AddressNumber_AddressNumber_whenFalse()
        {
            var operand1 = new AddressNumber("456", "A");
            var operand2 = new AddressNumber("123", "A");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_AddressNumber_AddressNumber_whenSame()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_AddressNumber_AddressNumber_whenTrue()
        {
            var operand1 = new AddressNumber("123", "A");
            var operand2 = new AddressNumber("456", "A");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void op_CompareTo_AddressNumber()
        {
            IComparable<AddressNumber> obj = new AddressNumber("123", "A");

            Assert.Equal(0, obj.CompareTo(new AddressNumber("123", "A")));
        }

        [Fact]
        public void op_CompareTo_AddressNumberNull()
        {
            IComparable<AddressNumber> obj = new AddressNumber();

            Assert.Throws<ArgumentNullException>(() => obj.CompareTo(null));
        }

        [Fact]
        public void op_CompareTo_AddressNumberSame()
        {
            var value = new AddressNumber("123", "A");
            IComparable<AddressNumber> obj = value;

            Assert.Equal(0, obj.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_AddressNumber_whenGreater()
        {
            IComparable<AddressNumber> obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber("6", string.Empty)) > 0);
        }

        [Fact]
        public void op_CompareTo_AddressNumber_whenLeftNotNumeric()
        {
            IComparable<AddressNumber> obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber(null, "A")) > 0);
        }

        [Fact]
        public void op_CompareTo_AddressNumber_whenLesser()
        {
            IComparable<AddressNumber> obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber("123", "A")) < 0);
        }

        [Fact]
        public void op_CompareTo_AddressNumber_whenNotNumeric()
        {
            IComparable<AddressNumber> obj = new AddressNumber(null, "A");

            Assert.True(obj.CompareTo(new AddressNumber(null, "A")) == 0);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            IComparable obj = new AddressNumber("123", "A");

            Assert.Equal(0, obj.CompareTo(new AddressNumber("123", "A")));
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            IComparable obj = new AddressNumber("123", "A");

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.CompareTo(123));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            IComparable obj = new AddressNumber();

            Assert.True(obj.CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var value = new AddressNumber("123", "A");
            IComparable obj = value;

            Assert.Equal(0, obj.CompareTo(value));
        }

        [Fact]
        public void op_CompareTo_object_whenGreater()
        {
            IComparable obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber("6", string.Empty)) > 0);
        }

        [Fact]
        public void op_CompareTo_object_whenLeftNotNumeric()
        {
            IComparable obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber(null, "A")) > 0);
        }

        [Fact]
        public void op_CompareTo_object_whenLesser()
        {
            IComparable obj = new AddressNumber("6", "A");

            Assert.True(obj.CompareTo(new AddressNumber("123", "A")) < 0);
        }

        [Fact]
        public void op_CompareTo_object_whenNotNumeric()
        {
            IComparable obj = new AddressNumber(null, "A");

            Assert.True(obj.CompareTo(new AddressNumber(null, "A")) == 0);
        }

        [Fact]
        public void op_CompareTo_object_whenRightNotNumeric()
        {
            IComparable obj = new AddressNumber(null, "A");

            Assert.True(obj.CompareTo(new AddressNumber("6", "A")) < 0);
        }

        [Fact]
        public void op_Compare_AddressNumberGreater_AddressNumber()
        {
            var comparand1 = new AddressNumber("456", "A");
            var comparand2 = new AddressNumber("123", "A");

            Assert.True(AddressNumber.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_AddressNumberLesser_AddressNumber()
        {
            var comparand1 = new AddressNumber("123", "A");
            var comparand2 = new AddressNumber("456", "A");

            Assert.True(AddressNumber.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_AddressNumber_AddressNumber_whenEqual()
        {
            var comparand1 = new AddressNumber("123", "A");
            var comparand2 = new AddressNumber("123", "A");

            Assert.Equal(0, AddressNumber.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_AddressNumber_AddressNumber_whenSame()
        {
            var comparand1 = new AddressNumber("123", "A");
            var comparand2 = comparand1;

            Assert.Equal(0, AddressNumber.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Equals_object()
        {
            var value = new AddressNumber("123", "A");

            Assert.True(new AddressNumber("123", "A").Equals(value));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new AddressNumber().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new AddressNumber("123", "A").Equals("123A"));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new AddressNumber("123", "A");
            var actual = AddressNumber.FromString("123A");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringA1()
        {
            var expected = new AddressNumber("A", "1");
            var actual = AddressNumber.FromString("A1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            var expected = new AddressNumber();
            var actual = AddressNumber.FromString(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => AddressNumber.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var value = new AddressNumber("123", "A");

            var expected = value.ToString().GetHashCode();
            var actual = value.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "123A";
            var actual = new AddressNumber("123", "A").ToString();

            Assert.Equal(expected, actual);
        }
    }
}