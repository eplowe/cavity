namespace Cavity
{
    using System.Collections.Generic;

    public static class Characters
    {
        private static readonly List<char> _currencySymbols = new List<char>
                                                                  {
                                                                      '¤', 
                                                                      '₳', 
                                                                      '฿', 
                                                                      '₵', 
                                                                      '¢', 
                                                                      '₡', 
                                                                      '₢', 
                                                                      '₠', 
                                                                      '$', 
                                                                      '₫', 
                                                                      '৳', 
                                                                      '₯', 
                                                                      '€', 
                                                                      'ƒ', 
                                                                      '₣', 
                                                                      '₲', 
                                                                      '₴', 
                                                                      '₭', 
                                                                      'ℳ', 
                                                                      '₥', 
                                                                      '₦', 
                                                                      '₧', 
                                                                      '₱', 
                                                                      '₰', 
                                                                      '£', 
                                                                      '₹', 
                                                                      '₨', 
                                                                      '₪', 
                                                                      '₸', 
                                                                      '₮', 
                                                                      '₩', 
                                                                      '¥', 
                                                                      '៛'
                                                                  };

        private static readonly List<char> _englishLowercase = new List<char>
                                                                   {
                                                                       'a', 
                                                                       'b', 
                                                                       'c', 
                                                                       'd', 
                                                                       'e', 
                                                                       'f', 
                                                                       'g', 
                                                                       'h', 
                                                                       'i', 
                                                                       'j', 
                                                                       'k', 
                                                                       'l', 
                                                                       'm', 
                                                                       'n', 
                                                                       'o', 
                                                                       'p', 
                                                                       'q', 
                                                                       'r', 
                                                                       's', 
                                                                       't', 
                                                                       'u', 
                                                                       'v', 
                                                                       'w', 
                                                                       'x', 
                                                                       'y', 
                                                                       'z'
                                                                   };

        private static readonly List<char> _englishUppercase = new List<char>
                                                                   {
                                                                       'A', 
                                                                       'B', 
                                                                       'C', 
                                                                       'D', 
                                                                       'E', 
                                                                       'F', 
                                                                       'G', 
                                                                       'H', 
                                                                       'I', 
                                                                       'J', 
                                                                       'K', 
                                                                       'L', 
                                                                       'M', 
                                                                       'N', 
                                                                       'O', 
                                                                       'P', 
                                                                       'Q', 
                                                                       'R', 
                                                                       'S', 
                                                                       'T', 
                                                                       'U', 
                                                                       'V', 
                                                                       'W', 
                                                                       'X', 
                                                                       'Y', 
                                                                       'Z'
                                                                   };

        private static readonly List<char> _fractionSymbols = new List<char>
                                                                  {
                                                                      '½', 
                                                                      '⅓', 
                                                                      '¼', 
                                                                      '⅕', 
                                                                      '⅙', 
                                                                      '⅛', 
                                                                      '⅔', 
                                                                      '⅖', 
                                                                      '¾', 
                                                                      '⅗', 
                                                                      '⅜', 
                                                                      '⅘', 
                                                                      '⅚', 
                                                                      '⅝', 
                                                                      '⅞', 
                                                                      '⁄'
                                                                  };

        private static readonly List<char> _mathematicalSymbols = new List<char>
                                                                      {
                                                                          '∞', 
                                                                          '%', 
                                                                          '‰', 
                                                                          '‱', 
                                                                          '+', 
                                                                          '−', 
                                                                          '-', 
                                                                          '±', 
                                                                          '∓', 
                                                                          '×', 
                                                                          '÷', 
                                                                          '⁄', 
                                                                          '∣', 
                                                                          '∤', 
                                                                          '=', 
                                                                          '≠', 
                                                                          '<', 
                                                                          '>', 
                                                                          '≪', 
                                                                          '≫', 
                                                                          '≺', 
                                                                          '∝', 
                                                                          '⊗', 
                                                                          '′', 
                                                                          '″', 
                                                                          '‴', 
                                                                          '√', 
                                                                          '#', 
                                                                          'ℵ', 
                                                                          'ℶ', 
                                                                          ':', 
                                                                          '!', 
                                                                          '~', 
                                                                          '≈', 
                                                                          '≀', 
                                                                          '◅', 
                                                                          '▻', 
                                                                          '⋉', 
                                                                          '⋈', 
                                                                          '∴', 
                                                                          '∵', 
                                                                          '■', 
                                                                          '□', 
                                                                          '∎', 
                                                                          '▮', 
                                                                          '‣', 
                                                                          '⇒', 
                                                                          '→', 
                                                                          '⊃', 
                                                                          '⇔', 
                                                                          '↔', 
                                                                          '¬', 
                                                                          '˜', 
                                                                          '∧', 
                                                                          '∨', 
                                                                          '⊕', 
                                                                          '⊻', 
                                                                          '∀', 
                                                                          '∃', 
                                                                          '≜', 
                                                                          '≝', 
                                                                          '≐', 
                                                                          '≅', 
                                                                          '≡', 
                                                                          '{', 
                                                                          '}', 
                                                                          '∅', 
                                                                          '∈', 
                                                                          '∉', 
                                                                          '⊆', 
                                                                          '⊂', 
                                                                          '⊇', 
                                                                          '⊃', 
                                                                          '∪', 
                                                                          '∩', 
                                                                          '∆', 
                                                                          '∖', 
                                                                          '→', 
                                                                          '↦', 
                                                                          '∘', 
                                                                          'ℕ', 
                                                                          'N', 
                                                                          'ℤ', 
                                                                          'Z', 
                                                                          'ℙ', 
                                                                          'P', 
                                                                          'ℚ', 
                                                                          'Q', 
                                                                          'ℝ', 
                                                                          'R', 
                                                                          'ℂ', 
                                                                          'C', 
                                                                          'ℍ', 
                                                                          'H', 
                                                                          'O', 
                                                                          '∑', 
                                                                          '∏', 
                                                                          '∐', 
                                                                          'Δ', 
                                                                          'δ', 
                                                                          '∂', 
                                                                          '∇', 
                                                                          '′', 
                                                                          '•', 
                                                                          '∫', 
                                                                          '∮', 
                                                                          'π', 
                                                                          'σ', 
                                                                          '†', 
                                                                          'T', 
                                                                          '⊤', 
                                                                          '⊥', 
                                                                          '⊧', 
                                                                          '⊢', 
                                                                          'o'
                                                                      };

        private static readonly List<char> _punctuation = new List<char>
                                                              {
                                                                  '„', 
                                                                  '“', 
                                                                  '”', 
                                                                  '‘', 
                                                                  '’', 
                                                                  '\'', 
                                                                  '"', 
                                                                  '‐', 
                                                                  '‒', 
                                                                  '–', 
                                                                  '—', 
                                                                  '―', 
                                                                  '…', 
                                                                  '!', 
                                                                  '?', 
                                                                  '.', 
                                                                  ':', 
                                                                  ';', 
                                                                  ',', 
                                                                  '[', 
                                                                  ']', 
                                                                  '(', 
                                                                  ')', 
                                                                  '{', 
                                                                  '}', 
                                                                  '⟨', 
                                                                  '⟩', 
                                                                  '«', 
                                                                  '»', 
                                                                  '/', 
                                                                  '⁄'
                                                              };

        private static readonly List<char> _typography = new List<char>
                                                             {
                                                                 '&', 
                                                                 '@', 
                                                                 '*', 
                                                                 '\\', 
                                                                 '•', 
                                                                 '^', 
                                                                 '†', 
                                                                 '‡', 
                                                                 '°', 
                                                                 '〃', 
                                                                 '¡', 
                                                                 '¿', 
                                                                 '#', 
                                                                 '№', 
                                                                 'º', 
                                                                 'ª', 
                                                                 '¶', 
                                                                 '§', 
                                                                 '~', 
                                                                 '_', 
                                                                 '¦', 
                                                                 '|', 
                                                                 '©', 
                                                                 '®', 
                                                                 '℗', 
                                                                 '℠', 
                                                                 '™', 
                                                                 '⁂', 
                                                                 '⊤', 
                                                                 '⊥', 
                                                                 '☞', 
                                                                 '∴', 
                                                                 '∵', 
                                                                 '‽', 
                                                                 '؟', 
                                                                 '◊', 
                                                                 '※', 
                                                                 '⁀'
                                                             };

        private static readonly List<char> _whiteSpace = new List<char>
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
                                                                 '\uFEFF', 
                                                                 //// Zero Width No-Break Space
                                                                 '·'
                                                                 //// Interpunct
                                                             };

        public static IList<char> CurrencySymbols
        {
            get
            {
                return _currencySymbols;
            }
        }

        public static IList<char> EnglishLowercase
        {
            get
            {
                return _englishLowercase;
            }
        }

        public static IList<char> EnglishUppercase
        {
            get
            {
                return _englishUppercase;
            }
        }

        public static IList<char> FractionSymbols
        {
            get
            {
                return _fractionSymbols;
            }
        }

        public static IList<char> MathematicalSymbols
        {
            get
            {
                return _mathematicalSymbols;
            }
        }

        public static IList<char> Punctuation
        {
            get
            {
                return _punctuation;
            }
        }

        public static IList<char> Typography
        {
            get
            {
                return _typography;
            }
        }

        public static IList<char> WhiteSpace
        {
            get
            {
                return _whiteSpace;
            }
        }
    }
}