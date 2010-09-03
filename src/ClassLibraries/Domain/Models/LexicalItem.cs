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
            return string.Equals(CanonicalForm, value)
                   || Synonyms.Contains(value);
        }

        public bool Contains(string value, StringComparison comparisonType)
        {
            return string.Equals(CanonicalForm, value, comparisonType)
                   || Synonyms.Any(synonym => string.Equals(synonym, value, comparisonType));
        }
    }
}