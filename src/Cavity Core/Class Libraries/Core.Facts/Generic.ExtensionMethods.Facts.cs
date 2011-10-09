namespace Cavity
{
    using Xunit;

    public sealed class GenericExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(GenericExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_In_charA_charsABC()
        {
            Assert.True('A'.In('A', 'B', 'C'));
        }

        [Fact]
        public void op_In_charZ_charsABC()
        {
            Assert.False('Z'.In('A', 'B', 'C'));
        }

        [Fact]
        public void op_In_charZ_charsNull()
        {
            Assert.False('Z'.In());
        }
    }
}