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
        public void op_IsWhiteSpace_char()
        {
            Assert.False('a'.IsWhiteSpace());
        }
    }
}