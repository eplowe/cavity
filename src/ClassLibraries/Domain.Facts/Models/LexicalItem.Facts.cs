namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class LexicalItemFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LexicalItem>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new LexicalItem("Example"));
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new LexicalItem("Example")
            {
                Synonyms =
                    {
                        "One",
                        "Two"
                    }
            };

            Assert.False(obj.Contains("Three"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            Assert.False(new LexicalItem("Example").Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_StringComparison()
        {
            Assert.False(new LexicalItem("Example").Contains(string.Empty, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new LexicalItem("Example").Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_StringComparison()
        {
            Assert.False(new LexicalItem("Example").Contains(null, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_string_StringComparison()
        {
            Assert.False(new LexicalItem("Example").Contains("EXAMPLE", StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_Contains_string_StringComparisonInvariantCultureIgnoreCase()
        {
            Assert.True(new LexicalItem("Example").Contains("EXAMPLE", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_string_whenMatchesCanonicalForm()
        {
            Assert.True(new LexicalItem("Example").Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenMatchesSynonym()
        {
            var obj = new LexicalItem("Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.True(obj.Contains("Bar"));
        }

        [Fact]
        public void prop_CanonicalForm()
        {
            Assert.NotNull(new PropertyExpectations<LexicalItem>("CanonicalForm")
                               .TypeIs<string>()
                               .ArgumentNullException()
                               .ArgumentOutOfRangeException(string.Empty)
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Synonyms()
        {
            Assert.NotNull(new PropertyExpectations<LexicalItem>("Synonyms")
                               .TypeIs<HashSet<string>>()
                               .DefaultValueIsNotNull()
                               .IsNotDecorated()
                               .Result);
        }
    }
}