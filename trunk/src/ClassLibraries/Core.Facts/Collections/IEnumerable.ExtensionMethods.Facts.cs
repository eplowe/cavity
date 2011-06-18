namespace Cavity.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    public sealed class IEnumerableExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IEnumerableExtensionMethods).IsStatic());
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

        [Fact]
        public void op_IsEmpty_IEnumerable()
        {
            var obj = new List<string>
            {
                "item"
            };

            Assert.False(obj.IsNullOrEmpty());
        }

        [Fact]
        public void op_IsEmpty_IEnumerableEmpty()
        {
            var obj = new List<string>();

            Assert.True(obj.IsNullOrEmpty());
        }

        [Fact]
        public void op_IsEmpty_IEnumerableNull()
        {
            Assert.True((null as IEnumerable).IsNullOrEmpty());
        }
    }
}