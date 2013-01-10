namespace Cavity.Text
{
    using System;
    using System.Text;

    using Xunit;
    using Xunit.Extensions;

    public sealed class StringBuilderExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(StringBuilderExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData(false, "")]
        [InlineData(true, "example")]
        public void op_ContainsText_StringBuilder(bool expected,
                                                  string value)
        {
            var actual = new StringBuilder(value).ContainsText();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsText_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as StringBuilder).ContainsText());
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(false, "example")]
        public void op_IsEmpty_StringBuilder(bool expected,
                                             string value)
        {
            var actual = new StringBuilder(value).IsEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_IsEmpty_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as StringBuilder).IsEmpty());
        }

        [Theory]
        [InlineData(false, "")]
        [InlineData(true, "example")]
        public void op_IsNotEmpty_StringBuilder(bool expected,
                                                string value)
        {
            var actual = new StringBuilder(value).IsNotEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_IsNotEmpty_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as StringBuilder).IsNotEmpty());
        }
    }
}