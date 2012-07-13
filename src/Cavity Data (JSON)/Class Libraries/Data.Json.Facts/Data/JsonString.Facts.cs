namespace Cavity.Data
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonStringFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonString>()
                            .DerivesFrom<JsonValue>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            const string expected = "Example";
            var actual = new JsonString(expected).Value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void ctor_stringEmpty(string value)
        {
            Assert.NotNull(new JsonString(value));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonString(null));
        }

        [Fact]
        public void ctor_string_DateTime()
        {
            var expected = new DateTime(2011, 7, 14, 19, 43, 37);
            var actual = new JsonString(expected).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2011-07-14T19:43:37Z")] // ISO 8601 (GMT)
        [InlineData("2011-07-14T20:43:37+0100")] // ISO 8601 (+TimeZone)
        [InlineData("2011-07-14 20:43:37 +0100")] // ISO 8601 (Ruby)
        [InlineData("Thu, 14 Jul 2011 19:43:37 GMT")] // RFC 1123
        [InlineData("\\/Date(1310672617000)\\/")] // .NET
        public void op_ToDateTime(string value)
        {
            var expected = new DateTime(2011, 7, 14, 19, 43, 37, DateTimeKind.Utc);
            var actual = new JsonString(value).ToDateTime();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<JsonString>(x => x.Value)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set(string.Empty)
                            .Set("Example")
                            .Result);
        }
    }
}