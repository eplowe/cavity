namespace Cavity.Linq
{
    using System.Collections.Generic;
    using Xunit;

    public sealed class EnumerableFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Enumerable).IsStatic());
        }

        [Fact]
        public void op_Concat_IEnumerableStringEmpty_char()
        {
            var array = new string[]
            {
            };

            var expected = string.Empty;
            var actual = array.Concat(',');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Concat_IEnumerableStringNull_char()
        {
            string expected = null;
            var actual = (null as IEnumerable<string>).Concat(',');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Concat_IEnumerableString_char()
        {
            var array = new[]
            {
                "a", "b", "c"
            };

            const string expected = "a,b,c";
            var actual = array.Concat(',');

            Assert.Equal(expected, actual);
        }
    }
}