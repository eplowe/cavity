namespace Cavity
{
    using System;
    using System.Xml;
    using Xunit;

    public sealed class ValueObjectFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<ValueObject<ValueObjectDerived>>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Implements<IComparable>()
                .Implements<IEquatable<ValueObjectDerived>>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ValueObjectDerived() as ValueObject<ValueObjectDerived>);
        }

        [Fact]
        public void opImplicit_stringNull_ValueObjectOfT()
        {
            string expected = null;
            string actual = null as ValueObjectDerived;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opImplicit_string_ValueObjectOfT()
        {
            string expected = "31/12/1999 00:00:00" + Environment.NewLine + "123";
            string actual = new ValueObjectDerived
            {
                DateTimeProperty = new DateTime(1999, 12, 31),
                Int32Property = 123
            };

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfTNull()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfTNull()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfTNull()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            ValueObjectDerived operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfTNull()
        {
            ValueObjectDerived operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenEqual()
        {
            ValueObjectDerived comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal<int>(0, ValueObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            ValueObjectDerived comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived comparand2 = comparand1;

            Assert.Equal<int>(0, ValueObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived comparand1 = null;
            ValueObjectDerived comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfTNull()
        {
            ValueObjectDerived comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived comparand2 = null;

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTGreater_ValueObjectOfT()
        {
            ValueObjectDerived comparand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTLesser_ValueObjectOfT()
        {
            ValueObjectDerived comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived comparand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(obj.CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal<int>(0, obj.CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_object()
        {
            ValueObjectDerived left = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived right = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal<int>(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            ValueObjectDerived left = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived right = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            ValueObjectDerived left = new ValueObjectDerived
            {
                Int32Property = 456
            };
            ValueObjectDerived right = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(left.CompareTo(right) > 0);
        }

        [Fact]
        public void op_CompareTo_objectInt32()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ValueObjectDerived().CompareTo(123));
        }

        [Fact]
        public void op_Equals_T()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                DateTimeProperty = DateTime.UtcNow,
                Int32Property = 123
            };

            ValueObjectDerived comparand = new ValueObjectDerived
            {
                DateTimeProperty = XmlConvert.ToDateTime(obj.DateTimeProperty.ToXmlString(), XmlDateTimeSerializationMode.Utc),
                Int32Property = 123
            };

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_TDiffers()
        {
            ValueObjectDerived obj = new ValueObjectDerived();

            ValueObjectDerived comparand = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False((obj as IEquatable<ValueObjectDerived>).Equals(comparand));
        }

        [Fact]
        public void op_Equals_TNull()
        {
            Assert.False((new ValueObjectDerived() as IEquatable<ValueObjectDerived>).Equals(null));
        }

        [Fact]
        public void op_Equals_TSame()
        {
            ValueObjectDerived obj = new ValueObjectDerived();

            Assert.True((obj as IEquatable<ValueObjectDerived>).Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            ValueObjectDerived comparand = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            ValueObjectDerived obj = new ValueObjectDerived();

            ValueObjectDerived comparand = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new ValueObjectDerived().Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            ValueObjectDerived obj = new ValueObjectDerived();

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                StringProperty = "foo"
            };

            Assert.False(obj.Equals("foo" as object));
        }

        [Fact]
        public void op_RegisterProperty_ExpressionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ValueObjectDerived().RegisterNullProperty());
        }

        [Fact]
        public void op_GetHashCode()
        {
            Assert.NotEqual<int>(0, new ValueObjectDerived().GetHashCode());
        }

        [Fact]
        public void op_ToString()
        {
            ValueObjectDerived obj = new ValueObjectDerived
            {
                DateTimeProperty = DateTime.Today,
                Int32Property = 123,
                StringProperty = "test"
            };

            string expected = string.Concat(DateTime.Today.ToString(), Environment.NewLine, "123", Environment.NewLine, "test");
            string actual = obj.ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}