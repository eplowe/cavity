namespace Cavity.Collections
{
    using System.Collections.Generic;
    using Cavity.Data;
    using Cavity.Globalization;
    using Xunit;

    public sealed class TranslationDictionaryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TranslationDictionary<int>>()
                            .DerivesFrom<Dictionary<Language, int>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TranslationDictionary<int>());
        }

        [Fact]
        public void op_Add_Translation()
        {
            var obj = new TranslationDictionary<int>
            {
                new Translation<int>(123, "en")
            };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Contains_Translation()
        {
            var item = new Translation<int>(123, "en");
            var obj = new TranslationDictionary<int>
            {
                item
            };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var obj = new TranslationDictionary<int>
            {
                new Translation<int>(123, "en")
            };

            foreach (var item in obj)
            {
                Assert.IsType<Translation<int>>(item);
                Assert.Equal(new Language("en"), item.Language);
                Assert.Equal(123, item.Value);
            }
        }

        [Fact]
        public void op_Remove_KeyStringPair()
        {
            var item = new Translation<int>(123, "en");
            var obj = new TranslationDictionary<int>
            {
                item
            };

            Assert.True(obj.Remove(item));
            Assert.Equal(0, obj.Count);
        }
    }
}