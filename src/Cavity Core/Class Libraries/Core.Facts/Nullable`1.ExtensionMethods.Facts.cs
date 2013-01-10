namespace Cavity
{
    using Xunit;

    public sealed class NullableOfTExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(NullableOfTExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_HasNoValue_whenFalse()
        {
            int? value = 123;

            Assert.False(value.HasNoValue());
        }

        [Fact]
        public void op_HasNoValue_whenTrue()
        {
            int? value = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.True(value.HasNoValue());
            // ReSharper restore ExpressionIsAlwaysNull
        }
    }
}