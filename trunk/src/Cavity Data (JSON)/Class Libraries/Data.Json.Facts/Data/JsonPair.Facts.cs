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
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set(string.Empty)
                            .Set("Example")
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonPair>(x => x.Value)
                            .TypeIs<JsonValue>()
                            .ArgumentNullException()
                            .Set(new Mock<JsonValue>().Object)
                            .IsNotDecorated()
                            .Result);
        }
    }
}