namespace Cavity.Text
{
    using System;
    using System.Text;

    using Xunit;

    public sealed class StringBuilderExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(StringBuilderExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ContainsText_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as StringBuilder).ContainsText());
        }

        [Fact]
        public void op_ContainsText_StringBuilder_whenFalse()
        {
            Assert.False(new StringBuilder().ContainsText());
        }

        [Fact]
        public void op_ContainsText_StringBuilder_whenTrue()
        {
            var obj = new StringBuilder();
            obj.Append("example");

            Assert.True(obj.ContainsText());
        }
    }
}