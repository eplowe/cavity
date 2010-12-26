namespace Cavity.Models
{
    using System;
    using System.Linq;
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
        public void ctor_ILexiconComparerNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new LexicalItem(null, "Example"));
        }

        [Fact]
        public void ctor_ILexiconComparer_string()
        {
            Assert.NotNull(new LexicalItem(StandardLexiconComparer.Ordinal, "Example"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "Example");

            Assert.False(obj.Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_whenSynonyms()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.False(obj.Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "Example");

            Assert.False(obj.Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_whenSynonyms()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.False(obj.Contains(null));
        }

        [Fact]
        public void op_Contains_string_whenMatchesSynonym()
        {
            var obj = new LexicalItem(StandardLexiconComparer.OrdinalIgnoreCase, "Example")
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
        public void op_Contains_string_whenOrdinalIgnoreCase()
        {
            var obj = new LexicalItem(StandardLexiconComparer.OrdinalIgnoreCase, "Example");

            Assert.True(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_Invoke_Func()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, string.Concat("Foo", '\u00A0', "Bar"))
            {
                Synonyms =
                    {
                        string.Concat("Left", '\u00A0', "Right")
                    }
            };

            obj.Invoke(x => x.NormalizeWhiteSpace());

            Assert.Equal("Foo Bar", obj.CanonicalForm);
            Assert.Equal("Left Right", obj.Synonyms.First());
        }

        [Fact]
        public void op_Invoke_FuncNull()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "Example");

            Assert.Throws<ArgumentNullException>(() => obj.Invoke(null));
        }

        [Fact]
        public void prop_CanonicalForm()
        {
            Assert.True(new PropertyExpectations<LexicalItem>(p => p.CanonicalForm)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Spellings()
        {
            var obj = new LexicalItem(StandardLexiconComparer.Ordinal, "a")
            {
                Synonyms =
                    {
                        "b",
                        "c"
                    }
            };

            const string expected = "abc";
            var actual = obj.Spellings.Aggregate<string, string>(null, (current, spelling) => current + spelling);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Synonyms()
        {
            Assert.True(new PropertyExpectations<LexicalItem>(p => p.Synonyms)
                            .TypeIs<SynonymCollection>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}