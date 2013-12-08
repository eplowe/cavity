namespace Cavity
{
    using Cavity.Models;
    using Xunit;

    public sealed class StringExtensionMethodsFacts
    {
        [Fact]
        public void op_ToDate_string()
        {
            var expected = BritishPostcode.FromString("AB1 2ZZ");
            var actual = "AB1 2ZZ".ToPostcode();

            Assert.Equal(expected, actual);
        }
    }
}