namespace Cavity
{
    using System;
    using Xunit;

    public sealed class ComparableObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ComparableObject>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IComparable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ComparableObjectDerived() as ComparableObject);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ComparableObjectDerived("foo");
            var operand2 = new ComparableObjectDerived("bar");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = new ComparableObjectDerived("value");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            var operand2 = new ComparableObjectDerived("value");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfTNull()
        {
            var operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ComparableObjectDerived("bar");
            var operand2 = new ComparableObjectDerived("foo");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ComparableObjectDerived("foo");
            var operand2 = new ComparableObjectDerived("bar");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opImplicit_stringNull_ValueObjectOfT()
        {
            string expected = null;
            string actual = null as ComparableObjectDerived;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_ValueObjectOfT()
        {
            const string expected = "value";
            string actual = new ComparableObjectDerived("value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = new ComparableObjectDerived("value");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ComparableObjectDerived("foo");
            var operand2 = new ComparableObjectDerived("bar");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            var operand2 = new ComparableObjectDerived("value");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfTNull()
        {
            var operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ComparableObjectDerived("foo");
            var operand2 = new ComparableObjectDerived("bar");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ComparableObjectDerived("value");
            var operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ComparableObjectDerived("bar");
            var operand2 = new ComparableObjectDerived("foo");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            var left = new ComparableObjectDerived("value");
            var right = new ComparableObjectDerived("value");

            Assert.Equal(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            var left = new ComparableObjectDerived("foo");
            var right = new ComparableObjectDerived("bar");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ComparableObjectDerived("123").CompareTo(123));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            var left = new ComparableObjectDerived("bar");
            var right = new ComparableObjectDerived("foo");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            var obj = new ComparableObjectDerived("value");

            Assert.True(obj.CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var obj = new ComparableObjectDerived("value");

            Assert.Equal(0, obj.CompareTo(obj));
        }

        [Fact]
        public void op_Compare_ValueObjectOfTGreater_ValueObjectOfT()
        {
            var comparand1 = new ComparableObjectDerived("foo");
            var comparand2 = new ComparableObjectDerived("bar");

            Assert.True(ComparableObject.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTLesser_ValueObjectOfT()
        {
            var comparand1 = new ComparableObjectDerived("bar");
            var comparand2 = new ComparableObjectDerived("foo");

            Assert.True(ComparableObject.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived comparand1 = null;
            var comparand2 = new ComparableObjectDerived("value");

            Assert.True(ComparableObject.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfTNull()
        {
            var comparand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived comparand2 = null;

            Assert.True(ComparableObject.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenEqual()
        {
            var comparand1 = new ComparableObjectDerived("value");
            var comparand2 = new ComparableObjectDerived("value");

            Assert.Equal(0, ComparableObject.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var comparand1 = new ComparableObjectDerived("value");
            var comparand2 = comparand1;

            Assert.Equal(0, ComparableObject.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new ComparableObjectDerived("value");
            var comparand = new ComparableObjectDerived("value");

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new ComparableObjectDerived();
            var comparand = new ComparableObjectDerived("value");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new ComparableObjectDerived().Equals(null));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            var obj = new ComparableObjectDerived("value");

            Assert.False(obj.Equals("value"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            Assert.NotEqual(0, new ComparableObjectDerived("value").GetHashCode());
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new ComparableObjectDerived() as ComparableObject;

            Assert.Equal(string.Empty, obj.ToString());
        }
    }
}