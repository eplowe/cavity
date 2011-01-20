namespace Cavity.Models
{
    using System;

    public static class LexiconStringExtensionMethods
    {
        public static string RemoveMatch(this string value,
                                         Lexicon lexicon)
        {
            if (null == value)
            {
                return null;
            }

            if (0 == value.Length)
            {
                return string.Empty;
            }

            if (null == lexicon)
            {
                throw new ArgumentNullException("lexicon");
            }

            return lexicon.Contains(value)
                       ? string.Empty
                       : value;
        }
    }
}