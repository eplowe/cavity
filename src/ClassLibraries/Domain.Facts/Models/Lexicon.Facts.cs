namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cavity.Collections.Generic;
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
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .Result);
        }

        [Fact]
        public void ctor_IComparerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(null));
        }

        [Fact]
        public void ctor_INormalizationComparer()
        {
            Assert.NotNull(new Lexicon(NormalizationComparer.Ordinal));
        }

        [Fact]
        public void indexer_stringNull_get()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            Assert.Throws<ArgumentNullException>(() => obj[null]);
        }

        [Fact]
        public void indexer_string_get()
        {
            const string expected = "Example";

            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(expected);

            var actual = obj[expected].CanonicalForm;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void indexer_string_getWhenNotFound()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            Assert.Null(obj["Example"]);
        }

        [Fact]
        public void indexer_string_getWhenSynonym()
        {
            const string expected = "Foo";
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(expected).Synonyms.Add("Bar");

            var actual = obj["Bar"].CanonicalForm;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Add_string()
        {
            const string expected = "Example";

            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(expected);

            var actual = obj.Items.First().CanonicalForm;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Lexicon(NormalizationComparer.Ordinal).Add(string.Empty));
        }

        [Fact]
        public void op_Add_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Add(null));
        }

        [Fact]
        public void op_Add_string_alreadyExists()
        {
            const string expected = "Example";

            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(expected);
            obj.Add(expected);

            Assert.Equal(1, obj.Items.Count());
        }

        [Fact]
        public void op_Add_string_alreadyExistsAsSynonym()
        {
            const string expected = "foo";

            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add("bar").Synonyms.Add(expected);

            obj.Add(expected);

            Assert.Equal(1, obj.Items.Count());
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add("Example");

            Assert.True(obj.Contains("Example"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_whenOrdinalComparer()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_whenOrdinalComparer()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains(null));
        }

        [Fact]
        public void op_Contains_string_whenEmpty()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenEmptyWithOrdinalComparer()
        {
            Assert.False(new Lexicon(NormalizationComparer.Ordinal).Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenOrdinalIgnoreCaseComparer()
        {
            var obj = new Lexicon(NormalizationComparer.OrdinalIgnoreCase);
            obj.Add("Example");

            Assert.True(obj.Contains("EXAMPLE"));
        }

        [Fact]
        public void op_Delete()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Delete());
        }

        [Fact]
        public void op_Delete_IStoreLexicon()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Delete(obj)).Verifiable();

            obj.Delete(storage.Object);

            Assert.Same(storage.Object, obj.Storage);

            storage.VerifyAll();
        }

        [Fact]
        public void op_Delete_IStoreLexiconNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Delete(null));
        }

        [Fact]
        public void op_Delete_whenStorageDefined()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Delete(obj)).Verifiable();

            obj.Storage = storage.Object;

            obj.Delete();

            storage.VerifyAll();
        }

        [Fact]
        public void op_Invoke_Func()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(string.Concat("Foo", '\u00A0', "Bar")).Synonyms.Add(string.Concat("Left", '\u00A0', "Right"));

            obj.Invoke(x => x.NormalizeWhiteSpace());

            Assert.Equal("Foo Bar", obj.Items.First().CanonicalForm);
            Assert.Equal("Left Right", obj.Items.First().Synonyms.First());
        }

        [Fact]
        public void op_Invoke_FuncNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Invoke(null));
        }

        [Fact]
        public void op_Remove_IEnumerableLexicalItems()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add("1").Synonyms.Add("One");

            var lexicon = new Lexicon(NormalizationComparer.Ordinal);
            lexicon.Add("1").Synonyms.Add("One");

            obj.Remove(lexicon.Items);

            Assert.Equal(0, obj.Items.Count());
        }

        [Fact]
        public void op_Remove_IEnumerableLexicalItemsEmpty()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add("1").Synonyms.Add("One");

            obj.Remove(new Lexicon(NormalizationComparer.Ordinal).Items);

            Assert.Equal(1, obj.Items.Count());
        }

        [Fact]
        public void op_Remove_IEnumerableLexicalItemsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Remove(null));
        }

        [Fact]
        public void op_Remove_IEnumerableLexicalItemsSynonym()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add("Foo").Synonyms.Add("One");

            var lexicon = new Lexicon(NormalizationComparer.Ordinal);
            lexicon.Add("Bar").Synonyms.Add("One");

            obj.Remove(lexicon.Items);

            Assert.Equal(0, obj.Items.Count());
        }

        [Fact]
        public void op_Save()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Save());
        }

        [Fact]
        public void op_Save_IStoreLexicon()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Save(obj)).Verifiable();

            obj.Save(storage.Object);

            Assert.Same(storage.Object, obj.Storage);

            storage.VerifyAll();
        }

        [Fact]
        public void op_Save_IStoreLexiconNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon(NormalizationComparer.Ordinal).Save(null));
        }

        [Fact]
        public void op_Save_whenStorageDefined()
        {
            var obj = new Lexicon(NormalizationComparer.Ordinal);

            var storage = new Mock<IStoreLexicon>();
            storage.Setup(x => x.Save(obj)).Verifiable();

            obj.Storage = storage.Object;

            obj.Save();

            storage.VerifyAll();
        }

        [Fact]
        public void op_ToCanonicalForm_string()
        {
            const string expected = "1";

            var obj = new Lexicon(NormalizationComparer.Ordinal);
            obj.Add(expected).Synonyms.Add("One");

            var actual = obj.ToCanonicalForm(obj.Items.First().Synonyms.First());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty()
        {
            Assert.Null(new Lexicon(NormalizationComparer.Ordinal).ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(NormalizationComparer.Ordinal).ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull()
        {
            Assert.Null(new Lexicon(NormalizationComparer.Ordinal).ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(NormalizationComparer.Ordinal).ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_string_whenOrdinalIgnoreCaseComparer()
        {
            const string expected = "1";

            var obj = new Lexicon(NormalizationComparer.OrdinalIgnoreCase);
            obj.Add(expected).Synonyms.Add("One");

            var actual = obj.ToCanonicalForm("ONE");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Items()
        {
            Assert.True(new PropertyExpectations<Lexicon>(p => p.Items)
                            .TypeIs<IEnumerable<LexicalItem>>()
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