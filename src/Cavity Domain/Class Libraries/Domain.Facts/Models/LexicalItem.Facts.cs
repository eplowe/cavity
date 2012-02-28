namespace Cavity.Models
{
    using System;
    using System.Linq;
    using Cavity.Collections;
    using Moq;
    using Xunit;

    public sealed class LexicalItemFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LexicalItem>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_INormalizationComparerNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new LexicalItem(null, "Example"));
        }

        [Fact]
        public void ctor_INormalizationComparer_string()
        {
            Assert.NotNull(new LexicalItem(NormalityComparer.Ordinal, "Example"));
        }

        [Fact]
        public void op_ContainsEnding_stringEmpty()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

            Assert.Null(obj.ContainsEnding(string.Empty));
        }

        [Fact]
        public void op_ContainsEnding_stringNull()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

            Assert.Throws<ArgumentNullException>(() => obj.ContainsEnding(null));
        }

        [Fact]
        public void op_ContainsEnding_string_whenExactCanonical()
        {
            const string expected = "Example";
            var obj = new LexicalItem(NormalityComparer.Ordinal, expected);
            var actual = obj.ContainsEnding(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsEnding_string_whenCaseDiffersCanonical()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

            Assert.Null(obj.ContainsEnding("EXAMPLE"));
        }

        [Fact]
        public void op_ContainsEnding_string_whenEndsWithCanonical()
        {
            const string expected = "example";
            var obj = new LexicalItem(NormalityComparer.OrdinalIgnoreCase, "EXAMPLE");
            var actual = obj.ContainsEnding("This is an example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsEnding_string_whenContainsCanonical()
        {
            const string expected = "example";
            var obj = new LexicalItem(NormalityComparer.OrdinalIgnoreCase, "EXAMPLE");
            var actual = obj.ContainsEnding("This is an example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsEnding_string_whenContainsSynonym()
        {
            const string expected = "example";
            var obj = new LexicalItem(NormalityComparer.OrdinalIgnoreCase, "ignore");
            obj.Synonyms.Add("EXAMPLE");

            var actual = obj.ContainsEnding("This is an {0}".FormatWith(expected));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsEnding_string_whenBadSpellingSynonym()
        {
            const string expected = "an ex_ample";
            var obj = new LexicalItem(new UnderscoreComparer(), "example");
            obj.Synonyms.Add("an example");

            var actual = obj.ContainsEnding("This is {0}".FormatWith(expected));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

            Assert.False(obj.Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_whenSynonyms()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example")
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
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

            Assert.Throws<ArgumentNullException>(() => obj.Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_whenSynonyms()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example")
            {
                Synonyms =
                    {
                        "Foo", 
                        "Bar"
                    }
            };

            Assert.Throws<ArgumentNullException>(() => obj.Contains(null));
        }

        [Fact]
        public void op_Contains_string_whenMatchesSynonym()
        {
            var obj = new LexicalItem(NormalityComparer.OrdinalIgnoreCase, "Example")
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
            var obj = new LexicalItem(NormalityComparer.OrdinalIgnoreCase, "Example");

            Assert.True(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_Invoke_Func()
        {
            var obj = new LexicalItem(NormalityComparer.Ordinal, string.Concat("Foo", '\u00A0', "Bar"))
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
            var obj = new LexicalItem(NormalityComparer.Ordinal, "Example");

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
            var obj = new LexicalItem(NormalityComparer.Ordinal, "a")
            {
                Synonyms =
                    {
                        "b", 
                        "c"
                    }
            };

            const string expected = "abc";
            var actual = obj
                .Spellings
                .Aggregate<string, string>(null, 
                                           (x, 
                                            spelling) => x + spelling);

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