namespace Cavity
{
    public static class CharExtensionMethods
    {
#if NET20
        public static bool IsCurrencySymbol(char value)
#else
        public static bool IsCurrencySymbol(this char value)
#endif
        {
            return Characters.CurrencySymbols.Contains(value);
        }

#if NET20
        public static bool IsDigit(char value)
#else
        public static bool IsDigit(this char value)
#endif
        {
            return char.IsDigit(value);
        }

#if NET20
        public static bool IsFractionSymbol(char value)
#else
        public static bool IsFractionSymbol(this char value)
#endif
        {
            return Characters.FractionSymbols.Contains(value);
        }

#if NET20
        public static bool IsLetter(char value)
#else
        public static bool IsLetter(this char value)
#endif
        {
            return char.IsLetter(value);
        }

#if NET20
        public static bool IsLetterOrDigit(char value)
#else
        public static bool IsLetterOrDigit(this char value)
#endif
        {
            return char.IsLetterOrDigit(value);
        }

#if NET20
        public static bool IsMathematicalSymbol(char value)
#else
        public static bool IsMathematicalSymbol(this char value)
#endif
        {
            return Characters.MathematicalSymbols.Contains(value);
        }

#if NET20
        public static bool IsNumber(char value)
#else
        public static bool IsNumber(this char value)
#endif
        {
            return char.IsNumber(value);
        }

#if NET20
        public static bool IsPunctuation(char value)
#else
        public static bool IsPunctuation(this char value)
#endif
        {
            return Characters.Punctuation.Contains(value);
        }

#if NET20
        public static bool IsSeparator(char value)
#else
        public static bool IsSeparator(this char value)
#endif
        {
            return char.IsSeparator(value);
        }

#if NET20
        public static bool IsSymbol(char value)
#else
        public static bool IsSymbol(this char value)
#endif
        {
            return char.IsSymbol(value);
        }

#if NET20
        public static bool IsTypography(char value)
#else
        public static bool IsTypography(this char value)
#endif
        {
            return Characters.Typography.Contains(value);
        }

#if NET20
        public static bool IsWhiteSpace(char value)
#else
        public static bool IsWhiteSpace(this char value)
#endif
        {
            return Characters.WhiteSpace.Contains(value);
        }

#if NET20
        public static char? ToEnglishAlphabet(char value)
#else
        public static char? ToEnglishAlphabet(this char value)
#endif
        {
            if (Characters.EnglishUppercase.Contains(value))
            {
                return value;
            }

            if (Characters.EnglishLowercase.Contains(value))
            {
                return value;
            }

            switch (value)
            {
                case 'À':
                case 'Â':
                case 'Æ':
                    return 'A';
                case 'à':
                case 'â':
                case 'æ':
                    return 'a';
                case 'Ç':
                    return 'C';
                case 'ç':
                    return 'c';
                case 'É':
                case 'È':
                case 'Ê':
                case 'Ë':
                    return 'E';
                case 'é':
                case 'è':
                case 'ê':
                case 'ë':
                    return 'e';
                case 'Î':
                case 'Ï':
                    return 'I';
                case 'î':
                case 'ï':
                    return 'i';
                case 'Ô':
                case 'Œ':
                    return 'O';
                case 'ô':
                case 'œ':
                    return 'o';
                case 'Ù':
                case 'Û':
                case 'Ü':
                    return 'U';
                case 'ù':
                case 'û':
                case 'ü':
                    return 'u';
                case 'Ÿ':
                    return 'Y';
                case 'ÿ':
                    return 'y';
                default:
                    return null;
            }
        }
    }
}