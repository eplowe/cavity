namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;

    public sealed class StandardLexiconComparer : ILexiconComparer
    {
        private static readonly StandardLexiconComparer _currentCulture = new StandardLexiconComparer(StringComparer.CurrentCulture);

        private static readonly StandardLexiconComparer _currentCultureIgnoreCase = new StandardLexiconComparer(StringComparer.CurrentCultureIgnoreCase);

        private static readonly StandardLexiconComparer _ordinal = new StandardLexiconComparer(StringComparer.Ordinal);

        private static readonly StandardLexiconComparer _ordinalIgnoreCase = new StandardLexiconComparer(StringComparer.OrdinalIgnoreCase);

        private IComparer<string> _comparer;

        public StandardLexiconComparer(IComparer<string> comparer)
            : this()
        {
            Comparer = comparer;
        }

        private StandardLexiconComparer()
        {
        }

        public static StandardLexiconComparer CurrentCulture
        {
            get
            {
                return _currentCulture;
            }
        }

        public static StandardLexiconComparer CurrentCultureIgnoreCase
        {
            get
            {
                return _currentCultureIgnoreCase;
            }
        }

        public static StandardLexiconComparer Ordinal
        {
            get
            {
                return _ordinal;
            }
        }

        public static StandardLexiconComparer OrdinalIgnoreCase
        {
            get
            {
                return _ordinalIgnoreCase;
            }
        }

        public IComparer<string> Comparer
        {
            get
            {
                return _comparer;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _comparer = value;
            }
        }

        int IComparer<string>.Compare(string x, string y)
        {
            return Comparer.Compare(x, y);
        }

        string ILexiconComparer.Normalize(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return StringComparer.CurrentCultureIgnoreCase.Equals(Comparer) ||
                   StringComparer.InvariantCultureIgnoreCase.Equals(Comparer) ||
                   StringComparer.OrdinalIgnoreCase.Equals(Comparer)
                       ? value.ToUpperInvariant()
                       : value;
        }
    }
}