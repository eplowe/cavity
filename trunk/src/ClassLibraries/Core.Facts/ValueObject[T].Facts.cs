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
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ValueObjectDerived() as ValueObject<ValueObjectDerived>);
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