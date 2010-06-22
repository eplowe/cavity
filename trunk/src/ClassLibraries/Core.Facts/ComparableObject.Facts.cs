namespace Cavity
{
    using System;
    using System.Xml;
    using Xunit;

    public sealed class ComparableObjectFacts
    {
        [Fact]
        public void type_definition()
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
        public void opImplicit_stringNull_ValueObjectOfT()
        {
            string expected = null;
            string actual = null as ComparableObjectDerived;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_ValueObjectOfT()
        {
            string expected = "value";
            string actual = new ComparableObjectDerived("value");

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("foo");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("bar");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfTNull()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("foo");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("bar");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfTNull()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("bar");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("foo");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("foo");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("bar");

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfTNull()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("foo");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("bar");

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("bar");
            ComparableObjectDerived operand2 = new ComparableObjectDerived("foo");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived operand1 = null;
            ComparableObjectDerived operand2 = new ComparableObjectDerived("value");

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfTNull()
        {
            ComparableObjectDerived operand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenEqual()
        {
            ComparableObjectDerived comparand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived comparand2 = new ComparableObjectDerived("value");

            Assert.Equal<int>(0, ComparableObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ComparableObjectDerived comparand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived comparand2 = comparand1;

            Assert.Equal<int>(0, ComparableObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfTNull_ValueObjectOfT()
        {
            ComparableObjectDerived comparand1 = null;
            ComparableObjectDerived comparand2 = new ComparableObjectDerived("value");

            Assert.True(ComparableObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfTNull()
        {
            ComparableObjectDerived comparand1 = new ComparableObjectDerived("value");
            ComparableObjectDerived comparand2 = null;

            Assert.True(ComparableObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTGreater_ValueObjectOfT()
        {
            ComparableObjectDerived comparand1 = new ComparableObjectDerived("foo");
            ComparableObjectDerived comparand2 = new ComparableObjectDerived("bar");

            Assert.True(ComparableObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTLesser_ValueObjectOfT()
        {
            ComparableObjectDerived comparand1 = new ComparableObjectDerived("bar");
            ComparableObjectDerived comparand2 = new ComparableObjectDerived("foo");

            Assert.True(ComparableObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived("value");

            Assert.True(obj.CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived("value");

            Assert.Equal<int>(0, obj.CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            ComparableObjectDerived left = new ComparableObjectDerived("value");
            ComparableObjectDerived right = new ComparableObjectDerived("value");

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            ComparableObjectDerived left = new ComparableObjectDerived("bar");
            ComparableObjectDerived right = new ComparableObjectDerived("foo");

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            ComparableObjectDerived left = new ComparableObjectDerived("foo");
            ComparableObjectDerived right = new ComparableObjectDerived("bar");

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ComparableObjectDerived("123").CompareTo(123));
        }

        [Fact]
        public void op_Equals_object()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived("value");
            ComparableObjectDerived comparand = new ComparableObjectDerived("value");

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived();

            ComparableObjectDerived comparand = new ComparableObjectDerived("value");

            Assert.False(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new ComparableObjectDerived().Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived();

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            ComparableObjectDerived obj = new ComparableObjectDerived("value");

            Assert.False(obj.Equals("value" as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            Assert.NotEqual<int>(0, new ComparableObjectDerived("value").GetHashCode());
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "value";

            ComparableObjectDerived obj = new ComparableObjectDerived(expected);

            string actual = obj.ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}