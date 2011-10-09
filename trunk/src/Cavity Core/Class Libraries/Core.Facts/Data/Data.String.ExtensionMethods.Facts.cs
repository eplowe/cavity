namespace Cavity.Data
{
    using Xunit;

    public sealed class DataStringExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(DataStringExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_string()
        {
            const string expected = "example";
            var actual = expected.FormatCommaSeparatedValue();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_stringComma()
        {
            const string expected = "\"foo, bar\"";
            var actual = "foo, bar".FormatCommaSeparatedValue();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_stringCommaQuote()
        {
            const string expected = "\"\"\"foo\"\", bar\"";
            var actual = "\"foo\", bar".FormatCommaSeparatedValue();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.FormatCommaSeparatedValue();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_stringNull()
        {
            Assert.Null((null as string).FormatCommaSeparatedValue());
        }

        [Fact]
        public void op_FormatCommaSeparatedValue_stringQuote()
        {
            const string expected = "\"a, \"\"b\"\", c\"";
            var actual = "a, \"b\", c".FormatCommaSeparatedValue();

            Assert.Equal(expected, actual);
        }
    }
}