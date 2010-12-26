namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class SynonymCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<SynonymCollection>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<string>>()
                            .Result);
        }

        [Fact]
        public void ctor_ILexiconComparer()
        {
            Assert.NotNull(new SynonymCollection(new ILexiconComparerDummy()));
        }

        [Fact]
        public void ctor_ILexiconComparerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SynonymCollection(null));
        }

        [Fact]
        public void op_Add_string()
        {
            var obj = new SynonymCollection(StandardLexiconComparer.OrdinalIgnoreCase)
            {
                "Example"
            };
        }

        [Fact]
        public void op_Clear()
        {
            var obj = new SynonymCollection(StandardLexiconComparer.OrdinalIgnoreCase)
            {
                "Example"
            };

            Assert.Equal(1, obj.Count);

            obj.Clear();

            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new SynonymCollection(StandardLexiconComparer.Ordinal)
            {
                "Example"
            };

            Assert.False(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_Contains_string_whenOrdinal()
        {
            const string expected = "Example";

            var obj = new SynonymCollection(StandardLexiconComparer.Ordinal)
            {
                expected
            };

            Assert.True(obj.Contains(expected));
        }

        [Fact]
        public void op_Contains_string_whenOrdinalIgnoreCase()
        {
            var obj = new SynonymCollection(StandardLexiconComparer.OrdinalIgnoreCase)
            {
                "Example"
            };

            Assert.True(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            const string expected = "Example";

            var obj = new SynonymCollection(StandardLexiconComparer.OrdinalIgnoreCase)
            {
                expected
            };

            foreach (var actual in obj)
            {
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void prop_Count()
        {
            var obj = new SynonymCollection(StandardLexiconComparer.OrdinalIgnoreCase);

            Assert.Equal(0, obj.Count);

            obj.Add("Example");

            Assert.Equal(1, obj.Count);
        }
    }
}