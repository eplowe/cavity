namespace Cavity.Collections
{
    using Xunit;

    public sealed class LevenshteinComparerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LevenshteinComparer>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<INormalizationComparer>()
                            .Result);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringEmpty()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength1()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength10()
        {
            const int expected = 3;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "1234567890");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength11()
        {
            const int expected = 3;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "12345678901");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength12()
        {
            const int expected = 4;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "123456789012");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength2()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "12");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength3()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "123");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength4()
        {
            const int expected = 1;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "1234");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength5()
        {
            const int expected = 1;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "12345");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength6()
        {
            const int expected = 2;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "123456");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength7()
        {
            const int expected = 2;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "1234567");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength8()
        {
            const int expected = 2;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "12345678");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringLength9()
        {
            const int expected = 3;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", "123456789");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CalculateThreshold_string_stringNull()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().CalculateThreshold("x", null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_string_string()
        {
            const int expected = -23;
            var actual = new DerivedLevenshteinComparer().Compare("abc", "xyz");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_string_string_whenAboveThreshold()
        {
            const int expected = -40;
            var actual = new DerivedLevenshteinComparer().Compare("00000000", "00xxxx00");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_string_string_whenBelowThreshold()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().Compare("00000000", "000xx000");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_string_string_whenEqual()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().Compare("00000000", "00000000");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_string_string_whenEqualIgnoreCase()
        {
            const int expected = 0;
            var actual = new DerivedLevenshteinComparer().Compare("abc", "ABC");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringAmpersand()
        {
            const string expected = "X AND Y";
            var actual = new DerivedLevenshteinComparer().Normalize("x & y");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringComma()
        {
            const string expected = "X Y";
            var actual = new DerivedLevenshteinComparer().Normalize("x,y");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringDash()
        {
            const string expected = "X Y";
            var actual = new DerivedLevenshteinComparer().Normalize("x-y");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringEmpty()
        {
            var expected = string.Empty;
            var actual = new DerivedLevenshteinComparer().Normalize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringNull()
        {
            var expected = string.Empty;
            var actual = new DerivedLevenshteinComparer().Normalize(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringPeriod()
        {
            var expected = string.Empty;
            var actual = new DerivedLevenshteinComparer().Normalize(".");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringSingleQuote()
        {
            var expected = string.Empty;
            var actual = new DerivedLevenshteinComparer().Normalize("'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Normalize_stringUpperTrim()
        {
            const string expected = "ABC";
            var actual = new DerivedLevenshteinComparer().Normalize(" abc ");

            Assert.Equal(expected, actual);
        }
    }
}