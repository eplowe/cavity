namespace Cavity
{
    using System.Collections.Generic;

    public static class WhiteSpace
    {
        private static readonly HashSet<char> _characters = new HashSet<char>
        {
            '\u0009',
            //// HT (Horizontal Tab)
            '\u000A',
            //// LF (Line Feed)
            '\u000B',
            //// VT (Vertical Tab)
            '\u000C',
            //// FF (Form Feed)
            '\u000D',
            //// CR (Carriage Return)
            '\u0020',
            //// Space
            '\u0085',
            //// NEL (control character next line)
            '\u00A0',
            //// No-Break Space
            '\u1680',
            //// Ogham Space Mark
            '\u180E',
            //// Mongolian Vowel Separator
            '\u2000',
            //// En quad
            '\u2001',
            //// Em quad
            '\u2002',
            //// En Space
            '\u2003',
            //// Em Space
            '\u2004',
            //// Three-Per-Em Space
            '\u2005',
            //// Four-Per-Em Space
            '\u2006',
            //// Six-Per-Em Space
            '\u2007',
            //// Figure Space
            '\u2008',
            //// Punctuation Space
            '\u2009',
            //// Thin Space
            '\u200A',
            //// Hair Space
            '\u200B',
            //// Zero Width Space
            '\u200C',
            //// Zero Width Non Joiner
            '\u200D',
            //// Zero Width Joiner
            '\u2028',
            //// Line Separator
            '\u2029',
            //// Paragraph Separator
            '\u202F',
            //// Narrow No-Break Space
            '\u205F',
            //// Medium Mathematical Space
            '\u2060',
            //// Word Joiner
            '\u3000',
            //// Ideographic Space
            '\uFEFF'
            //// Zero Width No-Break Space
        };

        public static HashSet<char> Characters
        {
            get
            {
                return _characters;
            }
        }
    }
}