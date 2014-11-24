namespace Cavity.Data
{
    using System;
    using System.Xml;
    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonNumberFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonNumber>()
                            .DerivesFrom<JsonValue>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_decimal()
        {
            const string expected = "1.23";
            var actual = new JsonNumber(1.23m).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_double()
        {
            const string expected = "1.23";
            var actual = new JsonNumber(1.23d).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_int()
        {
            const string expected = "123";
            var actual = new JsonNumber(123).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_long()
        {
            const string expected = "123";
            var actual = new JsonNumber(123L).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_short()
        {
            const short value = 123;
            const string expected = "123";
            var actual = new JsonNumber(value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_single()
        {
            const string expected = "1.2300000190734863";
            var actual = new JsonNumber(1.23f).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            const string expected = "123";
            var actual = new JsonNumber(expected).Value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void ctor_stringEmpty(string value)
        {
            Assert.NotNull(new JsonNumber(value));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonNumber(null));
        }

        [Fact]
        public void op_ToDecimal()
        {
            const decimal expected = 1.23m;
            var actual = new JsonNumber(XmlConvert.ToString(expected)).ToDecimal();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("123.45")]
        [InlineData("1.2345e2")]
        public void op_ToDouble(string value)
        {
            const double expected = 123.45;
            var actual = new JsonNumber(value).ToDouble();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt16()
        {
            const short expected = 1;
            var actual = new JsonNumber(XmlConvert.ToString(expected)).ToInt16();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt32()
        {
            const int expected = 123;
            var actual = new JsonNumber(XmlConvert.ToString(expected)).ToInt32();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt64()
        {
            const long expected = 12345;
            var actual = new JsonNumber(XmlConvert.ToString(expected)).ToInt64();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToSingle()
        {
            const float expected = 1.23f;
            var actual = new JsonNumber(XmlConvert.ToString(expected)).ToSingle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonNumber>(x => x.Value)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set(string.Empty)
                            .Set("Example")
                            .Result);
        }
    }
}