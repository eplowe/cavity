namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Xunit;
    using Xunit.Extensions;

    public sealed class IListExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IListExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_RemoveLast_IListOfTNull_int()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IList<long>).RemoveLast(123));
        }

        [Theory]
        [InlineData(2, "a,b", 1)]
        [InlineData(1, "a", 2)]
        [InlineData(0, "", 3)]
        [InlineData(0, "", 4)]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Tint", Justification = "There is an underscore.")]
        public void op_RemoveLast_IListOfT_int(int expected,
                                               string concat,
                                               int count)
        {
            var list = new List<string>
                {
                    "a",
                    "b",
                    "c"
                };

            var actual = list.RemoveLast(count);
            Assert.Equal(expected, actual.Count);

            Assert.Equal(concat, list.Concat(','));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Tint", Justification = "There is an underscore.")]
        public void op_RemoveLast_IListOfT_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new List<int>().RemoveLast(-1));
        }
    }
}