namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Data;
    using Moq;
    using Xunit;

    public sealed class LexiconFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Lexicon>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Lexicon());
        }

        [Fact]
        public void ctor_IComparer()
        {
            Assert.NotNull(new Lexicon(StringComparer.InvariantCulture));
        }

        [Fact]
        public void ctor_IComparerNull()
        {
            Assert.NotNull(new Lexicon(null));
        }

        [Fact]
        public void op_Add_string()
        {
            const string expected = "Example";

            var obj = new Lexicon();
            obj.Add(expected);

            var actual = obj.Items.First().CanonicalForm;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Lexicon().Add(string.Empty));
        }

        [Fact]
        public void op_Add_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon().Add(null));
        }

        [Fact]
        public void op_Add_string_alreadyExists()
        {
            const string expected = "Example";

            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem(expected));

            obj.Add(expected);

            Assert.Equal(1, obj.Items.Count);
        }

        [Fact]
        public void op_Add_string_alreadyExistsAsSynonym()
        {
            const string expected = "foo";

            var item = new LexicalItem("bar")
            {
                Synonyms =
                    {
                        expected
                    }
            };

            var obj = new Lexicon();
            obj.Items.Add(item);

            obj.Add(expected);

            Assert.Equal(1, obj.Items.Count);
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem("Example"));

            Assert.True(obj.Contains("Example"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            Assert.False(new Lexicon().Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_whenInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new Lexicon().Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_whenInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains(null));
        }

        [Fact]
        public void op_Contains_string_whenEmpty()
        {
            Assert.False(new Lexicon().Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenEmptyWithInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenInvariantComparer()
        {
            var obj = new Lexicon(StringComparer.InvariantCultureIgnoreCase);
            obj.Items.Add(new LexicalItem("Example"));

            Assert.True(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_Save()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon().Save());
        }

        [Fact]
        public void op_Save_whenStorageDefined()
        {
            var obj = new Lexicon();

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Save(obj)).Verifiable();

            obj.Storage = storage.Object;

            obj.Save();

            storage.VerifyAll();
        }

        [Fact]
        public void op_Save_IStoreLexiconNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon().Save(null));
        }

        [Fact]
        public void op_Save_IStoreLexicon()
        {
            var obj = new Lexicon();

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Save(obj)).Verifiable();

            obj.Save(storage.Object);

            Assert.Same(storage.Object, obj.Storage);

            storage.VerifyAll();
        }

        [Fact]
        public void op_ToCanonicalForm_string()
        {
            const string expected = "1";

            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem(expected)
            {
                Synonyms =
                    {
                        "One"
                    }
            });

            var actual = obj.ToCanonicalForm(obj.Items.First().Synonyms.First());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(StringComparer.InvariantCulture).ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(StringComparer.InvariantCulture).ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_string_whenInvariantComparer()
        {
            const string expected = "1";

            var obj = new Lexicon(StringComparer.InvariantCultureIgnoreCase);
            obj.Items.Add(new LexicalItem(expected)
            {
                Synonyms =
                    {
                        "One"
                    }
            });

            var actual = obj.ToCanonicalForm("ONE");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Comparer()
        {
            Assert.True(new PropertyExpectations<Lexicon>(p => p.Comparer)
                            .TypeIs<IComparer<string>>()
                            .DefaultValueIs(StringComparer.OrdinalIgnoreCase)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Items()
        {
            Assert.True(new PropertyExpectations<Lexicon>(p => p.Items)
                            .TypeIs<ICollection<LexicalItem>>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Storage()
        {
            Assert.True(new PropertyExpectations<Lexicon>(p => p.Storage)
                            .IsAutoProperty<IStoreLexicon>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}