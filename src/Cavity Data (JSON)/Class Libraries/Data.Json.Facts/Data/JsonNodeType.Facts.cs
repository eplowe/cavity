namespace Cavity.Data
{
    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonNodeTypeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonNodeType>()
                            .IsValueType()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonNodeType());
        }

        [Theory]
        [InlineData("None", 0)]
        [InlineData("Array", 1)]
        [InlineData("Object", 2)]
        [InlineData("Name", 3)]
        [InlineData("NullValue", 4)]
        [InlineData("TrueValue", 5)]
        [InlineData("FalseValue", 6)]
        [InlineData("NumberValue", 7)]
        [InlineData("StringValue", 8)]
        [InlineData("EndObject", 9)]
        [InlineData("EndArray", 10)]
        [InlineData("11", 11)]
        public void values(string expected, 
                           int value)
        {
            var actual = ((JsonNodeType)value).ToString();

            Assert.Equal(expected, actual);
        }
    }
}