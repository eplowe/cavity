﻿namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
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
        public void ctor()
        {
            Assert.NotNull(new LexicalItem("Example"));
        }

        [Fact]
        public void op_Invoke_Func()
        {
            var obj = new LexicalItem(string.Concat("Foo", '\u00A0', "Bar"))
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
            Assert.Throws<ArgumentNullException>(() => new LexicalItem("Example").Invoke(null));
        }

        [Fact]
        public void op_Contains_stringEmpty_IComparer()
        {
            var obj = new LexicalItem("Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.False(obj.Contains(string.Empty, StringComparer.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_stringEmpty_IComparerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new LexicalItem("Example").Contains(string.Empty, null));
        }

        [Fact]
        public void op_Contains_stringNull_IComparer()
        {
            var obj = new LexicalItem("Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.False(obj.Contains(null, StringComparer.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_stringNull_IComparerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new LexicalItem("Example").Contains(null, null));
        }

        [Fact]
        public void op_Contains_string_IComparerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new LexicalItem("Example").Contains("Example", null));
        }

        [Fact]
        public void op_Contains_string_IComparer_whenMatchesCanonicalForm()
        {
            Assert.True(new LexicalItem("Example").Contains("EXAMPLE", StringComparer.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_string_IComparer_whenMatchesSynonym()
        {
            var obj = new LexicalItem("Example")
            {
                Synonyms =
                    {
                        "Foo",
                        "Bar"
                    }
            };

            Assert.True(obj.Contains("Bar", StringComparer.InvariantCultureIgnoreCase));
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
            var obj = new LexicalItem("a")
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
                            .TypeIs<HashSet<string>>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }
    }
}