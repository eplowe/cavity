namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cavity.Collections.Generic;

    public sealed class LexicalItem
    {
        private KeyValuePair<string, string> _canonicalForm;

        private INormalizationComparer _comparer;

        public LexicalItem(INormalizationComparer comparer,
                           string canonicalForm)
            : this()
        {
            Comparer = comparer;
            Synonyms = new SynonymCollection(comparer);
            CanonicalForm = canonicalForm;
        }

        private LexicalItem()
        {
        }

        public string CanonicalForm
        {
            get
            {
                return _canonicalForm.Key;
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

                _canonicalForm = new KeyValuePair<string, string>(value, Comparer.Normalize(value));
            }
        }

        public IEnumerable<string> Spellings
        {
            get
            {
                if (null != CanonicalForm)
                {
                    yield return CanonicalForm;
                }

                foreach (var synonym in Synonyms)
                {
                    yield return synonym;
                }
            }
        }

        public SynonymCollection Synonyms { get; private set; }

        private INormalizationComparer Comparer
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

        public bool Contains(string value)
        {
            var x = _canonicalForm.Value;
            var y = Comparer.Normalize(value);

            return 0 == Comparer.Compare(x, y) || Synonyms.Contains(y);
        }

        public void Invoke(Func<string, string> function)
        {
            if (null == function)
            {
                throw new ArgumentNullException("function");
            }

            CanonicalForm = function.Invoke(CanonicalForm);

            var synonyms = Synonyms.ToList();
            Synonyms.Clear();
            foreach (var synonym in synonyms.OrderBy(x => x))
            {
                Synonyms.Add(function.Invoke(synonym));
            }
        }
    }
}