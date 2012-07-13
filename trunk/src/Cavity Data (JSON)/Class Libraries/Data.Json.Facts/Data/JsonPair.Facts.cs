namespace Cavity.Data
{
    using System;

    using Moq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonPairFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonPair>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_string_boolTrue()
        {
            Assert.IsType<JsonTrue>(new JsonPair("name", true).Value);
        }

        [Fact]
        public void ctor_string_boolFalse()
        {
            Assert.IsType<JsonFalse>(new JsonPair("name", false).Value);
        }

        [Fact]
        public void ctor_string_char0()
        {
            Assert.IsType<JsonNull>(new JsonPair("name", (char)0).Value);
        }

        [Fact]
        public void ctor_string_char()
        {
            Assert.Equal("a", ((JsonString)new JsonPair("name", 'a').Value).Value);
        }

        [Fact]
        public void ctor_string_DateTime()
        {
            const string expected = "1999-12-31T00:00:00Z";
            var actual = ((JsonString)new JsonPair("name", new DateTime(1999, 12, 31)).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_DateTimeOffset()
        {
            const string expected = "1999-12-31T00:00:00Z";
            var actual = ((JsonString)new JsonPair("name", new DateTimeOffset(new DateTime(1999, 12, 31))).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_decimal()
        {
            const string expected = "1.23";
            var actual = ((JsonNumber)new JsonPair("name", 1.23m).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_double()
        {
            const string expected = "1.23";
            var actual = ((JsonNumber)new JsonPair("name", 1.23d).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_int()
        {
            const string expected = "123";
            var actual = ((JsonNumber)new JsonPair("name", 123).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_long()
        {
            const string expected = "123";
            var actual = ((JsonNumber)new JsonPair("name", 123L).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_short()
        {
            const short value = 123;
            const string expected = "123";
            var actual = ((JsonNumber)new JsonPair("name", value).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string_single()
        {
            const string expected = "123";
            var actual = ((JsonNumber)new JsonPair("name", 123f).Value).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringNull_JsonValue()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonPair(null, new Mock<JsonValue>().Object));
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("Example")]
        public void ctor_string_JsonValue(string name)
        {
            var value = new Mock<JsonValue>().Object;
            var pair = new JsonPair(name, value);

            Assert.Equal(name, pair.Name);
            Assert.Same(value, pair.Value);
        }

        [Fact]
        public void ctor_string_JsonValueNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonPair("Example", null as JsonValue));
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<JsonPair>(x => x.Name)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set(string.Empty)
                            .Set("Example")
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonPair>(x => x.Value)
                            .IsNotDecorated()
                            .TypeIs<JsonValue>()
                            .ArgumentNullException()
                            .Set(new Mock<JsonValue>().Object)
                            .Result);
        }
    }
}