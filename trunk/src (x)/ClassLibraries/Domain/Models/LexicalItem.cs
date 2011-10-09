namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Collections.Generic;
    using Cavity.Data;

    public class LexicalItem
    {
        private KeyStringPair _canonicalForm;

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

                _canonicalForm = new KeyStringPair(value, Comparer.Normalize(value));
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

        public SynonymCollection Synonyms { get; protected set; }

        protected INormalizationComparer Comparer
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

        public virtual bool Contains(string value)
        {
            var x = _canonicalForm.Value;
            var y = Comparer.Normalize(value);

            return 0 == Comparer.Compare(x, y) || Synonyms.Contains(y);
        }

#if !NET20
        public virtual void Invoke(Func<string, string> func)
        {
            if (null == func)
            {
                throw new ArgumentNullException("func");
            }

            CanonicalForm = func.Invoke(CanonicalForm);
            var synonyms = Synonyms.ToList();
            Synonyms.Clear();
            foreach (var synonym in synonyms.OrderBy(x => x))
            {
                Synonyms.Add(func.Invoke(synonym));
            }
        }
#endif
    }
}