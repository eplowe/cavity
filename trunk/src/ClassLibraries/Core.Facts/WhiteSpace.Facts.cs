namespace Cavity
{
    using Xunit;

    public sealed class WhiteSpaceFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(WhiteSpace).IsStatic());
        }

        [Fact]
        public void prop_Characters()
        {
            Assert.True(WhiteSpace.Characters.Contains('\u0009')); // HT (Horizontal Tab)
            Assert.True(WhiteSpace.Characters.Contains('\u000A')); // LF (Line Feed)
            Assert.True(WhiteSpace.Characters.Contains('\u000B')); // VT (Vertical Tab)
            Assert.True(WhiteSpace.Characters.Contains('\u000C')); // FF (Form Feed)
            Assert.True(WhiteSpace.Characters.Contains('\u000D')); // CR (Carriage Return)
            Assert.True(WhiteSpace.Characters.Contains('\u0020')); // Space
            Assert.True(WhiteSpace.Characters.Contains('\u0085')); // NEL (control character next line)
            Assert.True(WhiteSpace.Characters.Contains('\u00A0')); // No-Break Space
            Assert.True(WhiteSpace.Characters.Contains('\u1680')); // Ogham Space Mark
            Assert.True(WhiteSpace.Characters.Contains('\u180E')); // Mongolian Vowel Separator
            Assert.True(WhiteSpace.Characters.Contains('\u2000')); // En quad
            Assert.True(WhiteSpace.Characters.Contains('\u2001')); // Em quad
            Assert.True(WhiteSpace.Characters.Contains('\u2002')); // En Space
            Assert.True(WhiteSpace.Characters.Contains('\u2003')); // Em Space
            Assert.True(WhiteSpace.Characters.Contains('\u2004')); // Three-Per-Em Space
            Assert.True(WhiteSpace.Characters.Contains('\u2005')); // Four-Per-Em Space
            Assert.True(WhiteSpace.Characters.Contains('\u2006')); // Six-Per-Em Space
            Assert.True(WhiteSpace.Characters.Contains('\u2007')); // Figure Space
            Assert.True(WhiteSpace.Characters.Contains('\u2008')); // Punctuation Space
            Assert.True(WhiteSpace.Characters.Contains('\u2009')); // Thin Space
            Assert.True(WhiteSpace.Characters.Contains('\u200A')); // Hair Space
            Assert.True(WhiteSpace.Characters.Contains('\u200B')); // Zero Width Space
            Assert.True(WhiteSpace.Characters.Contains('\u200C')); // Zero Width Non Joiner
            Assert.True(WhiteSpace.Characters.Contains('\u200D')); // Zero Width Joiner
            Assert.True(WhiteSpace.Characters.Contains('\u2028')); // Line Separator
            Assert.True(WhiteSpace.Characters.Contains('\u2029')); // Paragraph Separator
            Assert.True(WhiteSpace.Characters.Contains('\u202F')); // Narrow No-Break Space
            Assert.True(WhiteSpace.Characters.Contains('\u205F')); // Medium Mathematical Space
            Assert.True(WhiteSpace.Characters.Contains('\u2060')); // Word Joiner
            Assert.True(WhiteSpace.Characters.Contains('\u3000')); // Ideographic Space
            Assert.True(WhiteSpace.Characters.Contains('\uFEFF')); // Zero Width No-Break Space
        }
    }
}