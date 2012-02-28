namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Collections;
    using Cavity.Data;

    public class LexicalItem
    {
        private KeyStringPair _canonicalForm;

        private INormalityComparer _comparer;

        public LexicalItem(INormalityComparer comparer,
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
                return _canonicalForm.Value;
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

                _canonicalForm = new KeyStringPair(Comparer.Normalize(value), value);
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

        protected INormalityComparer Comparer
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

        public string ContainsEnding(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return null;
            }

            if (Contains(value))
            {
                return value;
            }

            var parts = value.Split(' ');
            if (parts.Length < 2)
            {
                return null;
            }

            var substring = value;
            foreach (var part in parts)
            {
#if NET20
                substring = StringExtensionMethods.RemoveFromStart(substring, part, StringComparison.Ordinal).TrimStart();
#else
                substring = substring.RemoveFromStart(part, StringComparison.Ordinal).TrimStart();
#endif
                if (Contains(substring))
                {
                    return substring;
                }
            }

            return null;
        }

        public virtual bool Contains(string value)
        {
            var x = _canonicalForm.Key;
            var y = Comparer.Normalize(value);

            return Comparer.Equals(x, y) || Synonyms.Contains(y);
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