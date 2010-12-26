namespace Cavity
{
    using Xunit;

    public sealed class CharExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(CharExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_IsIn_charA_charsABC()
        {
            Assert.True('A'.IsIn('A', 'B', 'C'));
        }

        [Fact]
        public void op_IsIn_charZ_charsABC()
        {
            Assert.False('Z'.IsIn('A', 'B', 'C'));
        }

        [Fact]
        public void op_IsIn_charZ_charsNull()
        {
            Assert.False('Z'.IsIn());
        }

        [Fact]
        public void op_IsWhiteSpace_char()
        {
            Assert.False('a'.IsWhiteSpace());
        }
    }
}