namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class StandardLexiconComparerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StandardLexiconComparer>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ILexiconComparer>()
                            .Result);
        }

        [Fact]
        public void ctor_IComparerOfString()
        {
            Assert.NotNull(new StandardLexiconComparer(StringComparer.Ordinal));
        }

        [Fact]
        public void ctor_IComparerOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StandardLexiconComparer(null));
        }

        [Fact]
        public void op_Compare_string_string()
        {
            IComparer<string> obj = new StandardLexiconComparer(StringComparer.Ordinal);

            var expected = StringComparer.Ordinal.Compare("x", "y");
            var actual = obj.Compare("x", "y");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringNull_whenOrdinalIgnoreCase()
        {
            ILexiconComparer obj = new StandardLexiconComparer(StringComparer.OrdinalIgnoreCase);

            Assert.Null(obj.Normalize(null));
        }

        [Fact]
        public void op_Normalize_string_whenCurrentCultureIgnoreCase()
        {
            ILexiconComparer obj = new StandardLexiconComparer(StringComparer.CurrentCultureIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_string_whenInvariantCultureIgnoreCase()
        {
            ILexiconComparer obj = new StandardLexiconComparer(StringComparer.InvariantCultureIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_string_whenOrdinal()
        {
            ILexiconComparer obj = new StandardLexiconComparer(StringComparer.Ordinal);

            const string expected = "Example";
            var actual = obj.Normalize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_string_whenOrdinalIgnoreCase()
        {
            ILexiconComparer obj = new StandardLexiconComparer(StringComparer.OrdinalIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Comparer()
        {
            Assert.NotNull(new PropertyExpectations<StandardLexiconComparer>(x => x.Comparer)
                               .TypeIs<IComparer<string>>()
                               .ArgumentNullException()
                               .Set(StringComparer.Ordinal)
                               .Result);
        }

        [Fact]
        public void prop_CurrentCulture()
        {
            var expected = StringComparer.CurrentCulture;
            var actual = StandardLexiconComparer.CurrentCulture.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_CurrentCultureIgnoreCase()
        {
            var expected = StringComparer.CurrentCultureIgnoreCase;
            var actual = StandardLexiconComparer.CurrentCultureIgnoreCase.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Ordinal()
        {
            var expected = StringComparer.Ordinal;
            var actual = StandardLexiconComparer.Ordinal.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_OrdinalIgnoreCase()
        {
            var expected = StringComparer.OrdinalIgnoreCase;
            var actual = StandardLexiconComparer.OrdinalIgnoreCase.Comparer;

            Assert.Equal(expected, actual);
        }
    }
}