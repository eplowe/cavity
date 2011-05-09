namespace Cavity.Collections
{
    using System.Collections.Generic;
    using Cavity.Data;
    using Xunit;

    public sealed class KeyStringDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<KeyStringDictionary>()
                            .DerivesFrom<Dictionary<string, string>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new KeyStringDictionary());
        }

        [Fact]
        public void op_Add_KeyStringPair()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", "value")
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Contains_KeyStringPair()
        {
            var item = new KeyStringPair("key", "value");
            var obj = new KeyStringDictionary
            {
                item
            };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var obj = new KeyStringDictionary
            {
                new KeyStringPair("key", "value")
            };

            foreach (var item in obj)
            {
                Assert.IsType<KeyStringPair>(item);
                Assert.Equal("key", item.Key);
                Assert.Equal("value", item.Value);
            }
        }

        [Fact]
        public void op_Remove_KeyStringPair()
        {
            var item = new KeyStringPair("key", "value");
            var obj = new KeyStringDictionary
            {
                item
            };

            Assert.True(obj.Remove(item));
            Assert.Equal(0, obj.Count);
        }
    }
}