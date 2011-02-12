namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    public sealed class NormalizationComparerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NormalizationComparer>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<INormalizationComparer>()
                            .Result);
        }

        [Fact]
        public void ctor_IComparerOfString()
        {
            Assert.NotNull(new NormalizationComparer(StringComparer.Ordinal));
        }

        [Fact]
        public void ctor_IComparerOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NormalizationComparer(null));
        }

        [Fact]
        public void op_Compare_string_string()
        {
            IComparer<string> obj = new NormalizationComparer(StringComparer.Ordinal);

            var expected = StringComparer.Ordinal.Compare("x", "y");
            var actual = obj.Compare("x", "y");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringNull_whenOrdinalIgnoreCase()
        {
            INormalizationComparer obj = new NormalizationComparer(StringComparer.OrdinalIgnoreCase);

            Assert.Null(obj.Normalize(null));
        }

        [Fact]
        public void op_Normalize_string_whenCurrentCultureIgnoreCase()
        {
            INormalizationComparer obj = new NormalizationComparer(StringComparer.CurrentCultureIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "Cavity.Collections.Generic.NormalizationComparer.#ctor(System.Collections.Generic.IComparer`1<System.String>)", Justification = "This is for testing purposes.")]
        public void op_Normalize_string_whenInvariantCultureIgnoreCase()
        {
            INormalizationComparer obj = new NormalizationComparer(StringComparer.InvariantCultureIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_string_whenOrdinal()
        {
            INormalizationComparer obj = new NormalizationComparer(StringComparer.Ordinal);

            const string expected = "Example";
            var actual = obj.Normalize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_string_whenOrdinalIgnoreCase()
        {
            INormalizationComparer obj = new NormalizationComparer(StringComparer.OrdinalIgnoreCase);

            const string expected = "EXAMPLE";
            var actual = obj.Normalize("Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Comparer()
        {
            Assert.NotNull(new PropertyExpectations<NormalizationComparer>(x => x.Comparer)
                               .TypeIs<IComparer<string>>()
                               .ArgumentNullException()
                               .Set(StringComparer.Ordinal)
                               .Result);
        }

        [Fact]
        public void prop_CurrentCulture()
        {
            var expected = StringComparer.CurrentCulture;
            var actual = NormalizationComparer.CurrentCulture.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_CurrentCultureIgnoreCase()
        {
            var expected = StringComparer.CurrentCultureIgnoreCase;
            var actual = NormalizationComparer.CurrentCultureIgnoreCase.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Ordinal()
        {
            var expected = StringComparer.Ordinal;
            var actual = NormalizationComparer.Ordinal.Comparer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_OrdinalIgnoreCase()
        {
            var expected = StringComparer.OrdinalIgnoreCase;
            var actual = NormalizationComparer.OrdinalIgnoreCase.Comparer;

            Assert.Equal(expected, actual);
        }
    }
}