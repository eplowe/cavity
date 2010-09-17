namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class LexicalItem
    {
        private string _canonicalForm;

        public LexicalItem(string canonicalForm)
            : this()
        {
            CanonicalForm = canonicalForm;
        }

        private LexicalItem()
        {
            Synonyms = new HashSet<string>();
        }

        public string CanonicalForm
        {
            get
            {
                return _canonicalForm;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                value = value.Trim();
                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _canonicalForm = value;
            }
        }

        public HashSet<string> Synonyms { get; private set; }

        public bool Contains(string value)
        {
            return Contains(value, null);
        }

        public bool Contains(string value, IComparer<string> comparer)
        {
            if (null == comparer)
            {
                return string.Equals(CanonicalForm, value) || Synonyms.Contains(value);
            }

            return 0 == comparer.Compare(CanonicalForm, value)
                   || Synonyms.Any(synonym => 0 == comparer.Compare(synonym, value));
        }
    }
}