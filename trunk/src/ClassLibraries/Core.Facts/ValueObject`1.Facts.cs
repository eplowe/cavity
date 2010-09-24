namespace Cavity
{
    using System;
    using System.Xml;
    using Xunit;

    public sealed class ValueObjectOfTFacts
    {
        [Fact]
        public void a_definition()
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
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = operand1;

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opEquality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 == operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfTNull()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = operand1;

            Assert.False(operand1 > operand2);
        }

        [Fact]
        public void opGreater_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 > operand2);
        }

        [Fact]
        public void opImplicit_stringNull_ValueObjectOfT()
        {
            string expected = null;
            string actual = null as ValueObjectDerived;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_ValueObjectOfT()
        {
            var expected = "31/12/1999 00:00:00" + Environment.NewLine + "123";
            var actual = new ValueObjectDerived
            {
                DateTimeProperty = new DateTime(1999, 12, 31),
                Int32Property = 123
            };

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = operand1;

            Assert.False(operand1 != operand2);
        }

        [Fact]
        public void opInequality_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 != operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived operand1 = null;
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfTNull()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived operand2 = null;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenFalse()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = operand1;

            Assert.False(operand1 < operand2);
        }

        [Fact]
        public void opLesser_ValueObjectOfT_ValueObjectOfT_whenTrue()
        {
            var operand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var operand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(operand1 < operand2);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            var left = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var right = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal(0, left.CompareTo(right));
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            var left = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var right = new ValueObjectDerived
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
        public void op_CompareTo_objectLesser()
        {
            var left = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var right = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(left.CompareTo(right) < 0);
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            var obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(obj.CompareTo(null) > 0);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal(0, obj.CompareTo(obj));
        }

        [Fact]
        public void op_Compare_ValueObjectOfTGreater_ValueObjectOfT()
        {
            var comparand1 = new ValueObjectDerived
            {
                Int32Property = 456
            };
            var comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTLesser_ValueObjectOfT()
        {
            var comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var comparand2 = new ValueObjectDerived
            {
                Int32Property = 456
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfTNull_ValueObjectOfT()
        {
            ValueObjectDerived comparand1 = null;
            var comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) < 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfTNull()
        {
            var comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            ValueObjectDerived comparand2 = null;

            Assert.True(ValueObjectDerived.Compare(comparand1, comparand2) > 0);
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenEqual()
        {
            var comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var comparand2 = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.Equal(0, ValueObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Compare_ValueObjectOfT_ValueObjectOfT_whenSame()
        {
            var comparand1 = new ValueObjectDerived
            {
                Int32Property = 123
            };
            var comparand2 = comparand1;

            Assert.Equal(0, ValueObjectDerived.Compare(comparand1, comparand2));
        }

        [Fact]
        public void op_Equals_T()
        {
            var obj = new ValueObjectDerived
            {
                DateTimeProperty = new DateTime(1999, 12, 31),
                Int32Property = 123
            };

            var comparand = new ValueObjectDerived
            {
                DateTimeProperty = XmlConvert.ToDateTime(obj.DateTimeProperty.ToXmlString(), XmlDateTimeSerializationMode.Utc),
                Int32Property = 123
            };

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_TDiffers()
        {
            var obj = new ValueObjectDerived();

            var comparand = new ValueObjectDerived
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
            var obj = new ValueObjectDerived();

            Assert.True((obj as IEquatable<ValueObjectDerived>).Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new ValueObjectDerived
            {
                Int32Property = 123
            };

            var comparand = new ValueObjectDerived
            {
                Int32Property = 123
            };

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new ValueObjectDerived();

            var comparand = new ValueObjectDerived
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
            var obj = new ValueObjectDerived();

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            var obj = new ValueObjectDerived
            {
                StringProperty = "foo"
            };

            Assert.False(obj.Equals("foo"));
        }

        [Fact]
        public void op_GetHashCode()
        {
            Assert.NotEqual(0, new ValueObjectDerived().GetHashCode());
        }

        [Fact]
        public void op_RegisterProperty_ExpressionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ValueObjectDerived().RegisterNullProperty());
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new ValueObjectDerived
            {
                DateTimeProperty = new DateTime(1999, 12, 31),
                Int32Property = 123,
                StringProperty = "test"
            };

            var expected = string.Concat(new DateTime(1999, 12, 31).ToString(), Environment.NewLine, "123", Environment.NewLine, "test");
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }
    }
}