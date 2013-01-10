namespace Cavity
{
    using Xunit;
    using Xunit.Extensions;

    public sealed class BooleanExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(BooleanExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        public void op_And_bool_bool(bool expected,
                                     bool value,
                                     bool comparand)
        {
            var actual = value.And(comparand);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        public void op_IsFalse_bool(bool expected,
                                    bool value)
        {
            var actual = value.IsFalse();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void op_IsTrue_bool(bool expected,
                                   bool value)
        {
            var actual = value.IsTrue();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void op_Or_bool_bool(bool expected,
                                    bool value,
                                    bool comparand)
        {
            var actual = value.Or(comparand);

            Assert.Equal(expected, actual);
        }
    }
}