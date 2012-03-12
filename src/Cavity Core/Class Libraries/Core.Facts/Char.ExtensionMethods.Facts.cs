namespace Cavity
{
    using System.Xml;
    using Xunit;
    using Xunit.Extensions;

    public sealed class CharExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(CharExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData("¤₳฿₵¢₡₢₠$₫৳₯€ƒ₣₲₴₭ℳ₥₦₧₱₰£₹₨₪₸₮₩¥៛")]
        public void op_IsCurrencySymbol_char(string values)
        {
            foreach (var value in values ?? string.Empty)
            {
                Assert.True(value.IsCurrencySymbol(), XmlConvert.ToString(value));
            }
        }

        [Theory]
        [InlineData("½⅓¼⅕⅙⅛⅔⅖¾⅗⅜⅘⅚⅝⅞⁄")]
        public void op_IsFractionSymbol_char(string values)
        {
            foreach (var value in values ?? string.Empty)
            {
                Assert.True(value.IsFractionSymbol(), XmlConvert.ToString(value));
            }
        }

        [Theory]
        [InlineData("∞%‰‱+−-±∓×÷⁄∣∤=≠<>≪≫≺∝⊗′″‴√#ℵℶ:!~≈≀◅▻⋉⋈∴∵■□∎▮‣⇒→⊃⇔↔¬˜∧∨⊕⊻∀∃≜≝≐≅≡{}∅∈∉⊆⊂⊇⊃∪∩∆∖→↦∘ℕNℤZℙPℚQℝRℂCℍHO∑∏∐Δδ∂∇′•∫∮πσ†T⊤⊥⊧⊢o")]
        public void op_IsMathematicalSymbol_char(string values)
        {
            foreach (var value in values ?? string.Empty)
            {
                Assert.True(value.IsMathematicalSymbol(), XmlConvert.ToString(value));
            }
        }

        [Theory]
        [InlineData("„“”‘’'\"‐‒–—―…!?.:;,[](){}⟨⟩«»/⁄")]
        public void op_IsPunctuation_char(string values)
        {
            foreach (var value in values ?? string.Empty)
            {
                Assert.True(value.IsPunctuation(), XmlConvert.ToString(value));
            }
        }

        [Theory]
        [InlineData("&@*\\•^†‡°〃¡¿#№ºª¶§~_¦|©®℗℠™⁂⊤⊥☞∴∵‽؟◊※⁀")]
        public void op_IsTypography_char(string values)
        {
            foreach (var value in values ?? string.Empty)
            {
                Assert.True(value.IsTypography(), XmlConvert.ToString(value));
            }
        }

        [Theory]
        [InlineData('A', "ÀÂÆ")]
        [InlineData('a', "àâæ")]
        [InlineData('C', "Ç")]
        [InlineData('c', "ç")]
        [InlineData('E', "ÉÈÊË")]
        [InlineData('e', "éèêë")]
        [InlineData('I', "ÎÏ")]
        [InlineData('i', "îï")]
        [InlineData('O', "ÔŒ")]
        [InlineData('o', "ôœ")]
        [InlineData('U', "ÙÛÜ")]
        [InlineData('u', "ùûü")]
        [InlineData('Y', "Ÿ")]
        [InlineData('y', "ÿ")]
        public void op_ToEnglishAlphabet_char(char expected, string values)
        {
            Assert.Equal(expected, expected.ToEnglishAlphabet());
            foreach (var value in values ?? string.Empty)
            {
                Assert.Equal(expected, value.ToEnglishAlphabet());
            }
        }

        [Fact]
        public void op_IsWhiteSpace_char()
        {
            Assert.False('a'.IsWhiteSpace());
        }
    }
}