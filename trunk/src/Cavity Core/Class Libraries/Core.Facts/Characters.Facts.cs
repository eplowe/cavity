namespace Cavity
{
    using System.Xml;
    using Xunit;

    public sealed class CharactersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Characters).IsStatic());
        }

        [Fact]
        public void prop_CurrencySymbols()
        {
            foreach (var c in "¤₳฿₵¢₡₢₠$₫৳₯€ƒ₣₲₴₭ℳ₥₦₧₱₰£₹₨₪₸₮₩¥៛")
            {
                Assert.True(Characters.CurrencySymbols.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_EnglishLowercase()
        {
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                Assert.True(Characters.EnglishLowercase.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_EnglishUppercase()
        {
            foreach (var c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            {
                Assert.True(Characters.EnglishUppercase.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_FractionSymbols()
        {
            foreach (var c in "½⅓¼⅕⅙⅛⅔⅖¾⅗⅜⅘⅚⅝⅞⁄")
            {
                Assert.True(Characters.FractionSymbols.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_MathematicalSymbols()
        {
            foreach (var c in "∞%‰‱+−-±∓×÷⁄∣∤=≠<>≪≫≺∝⊗′″‴√#ℵℶ:!~≈≀◅▻⋉⋈∴∵■□∎▮‣⇒→⊃⇔↔¬˜∧∨⊕⊻∀∃≜≝≐≅≡{}∅∈∉⊆⊂⊇⊃∪∩∆∖→↦∘ℕNℤZℙPℚQℝRℂCℍHO∑∏∐Δδ∂∇′•∫∮πσ†T⊤⊥⊧⊢o")
            {
                Assert.True(Characters.MathematicalSymbols.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_Punctuation()
        {
            foreach (var c in "„“”‘’'\"‐‒–—―…!?.:;,[](){}⟨⟩«»/⁄")
            {
                Assert.True(Characters.Punctuation.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_Typography()
        {
            foreach (var c in "&@*\\•^†‡°〃¡¿#№ºª¶§~_¦|©®℗℠™⁂⊤⊥☞∴∵‽؟◊※⁀")
            {
                Assert.True(Characters.Typography.Contains(c), XmlConvert.ToString(c));
            }
        }

        [Fact]
        public void prop_WhiteSpace()
        {
            Assert.True(Characters.WhiteSpace.Contains('\u0009')); // HT (Horizontal Tab)
            Assert.True(Characters.WhiteSpace.Contains('\u000A')); // LF (Line Feed)
            Assert.True(Characters.WhiteSpace.Contains('\u000B')); // VT (Vertical Tab)
            Assert.True(Characters.WhiteSpace.Contains('\u000C')); // FF (Form Feed)
            Assert.True(Characters.WhiteSpace.Contains('\u000D')); // CR (Carriage Return)
            Assert.True(Characters.WhiteSpace.Contains('\u0020')); // Space
            Assert.True(Characters.WhiteSpace.Contains('\u0085')); // NEL (control character next line)
            Assert.True(Characters.WhiteSpace.Contains('\u00A0')); // No-Break Space
            Assert.True(Characters.WhiteSpace.Contains('\u1680')); // Ogham Space Mark
            Assert.True(Characters.WhiteSpace.Contains('\u180E')); // Mongolian Vowel Separator
            Assert.True(Characters.WhiteSpace.Contains('\u2000')); // En quad
            Assert.True(Characters.WhiteSpace.Contains('\u2001')); // Em quad
            Assert.True(Characters.WhiteSpace.Contains('\u2002')); // En Space
            Assert.True(Characters.WhiteSpace.Contains('\u2003')); // Em Space
            Assert.True(Characters.WhiteSpace.Contains('\u2004')); // Three-Per-Em Space
            Assert.True(Characters.WhiteSpace.Contains('\u2005')); // Four-Per-Em Space
            Assert.True(Characters.WhiteSpace.Contains('\u2006')); // Six-Per-Em Space
            Assert.True(Characters.WhiteSpace.Contains('\u2007')); // Figure Space
            Assert.True(Characters.WhiteSpace.Contains('\u2008')); // Punctuation Space
            Assert.True(Characters.WhiteSpace.Contains('\u2009')); // Thin Space
            Assert.True(Characters.WhiteSpace.Contains('\u200A')); // Hair Space
            Assert.True(Characters.WhiteSpace.Contains('\u200B')); // Zero Width Space
            Assert.True(Characters.WhiteSpace.Contains('\u200C')); // Zero Width Non Joiner
            Assert.True(Characters.WhiteSpace.Contains('\u200D')); // Zero Width Joiner
            Assert.True(Characters.WhiteSpace.Contains('\u2028')); // Line Separator
            Assert.True(Characters.WhiteSpace.Contains('\u2029')); // Paragraph Separator
            Assert.True(Characters.WhiteSpace.Contains('\u202F')); // Narrow No-Break Space
            Assert.True(Characters.WhiteSpace.Contains('\u205F')); // Medium Mathematical Space
            Assert.True(Characters.WhiteSpace.Contains('\u2060')); // Word Joiner
            Assert.True(Characters.WhiteSpace.Contains('\u3000')); // Ideographic Space
            Assert.True(Characters.WhiteSpace.Contains('\uFEFF')); // Zero Width No-Break Space
        }
    }
}