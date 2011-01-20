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