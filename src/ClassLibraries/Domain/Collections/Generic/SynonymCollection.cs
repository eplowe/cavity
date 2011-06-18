namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Data;

    public sealed class SynonymCollection : IEnumerable<string>
    {
        private INormalizationComparer _comparer;

        public SynonymCollection(INormalizationComparer comparer)
            : this()
        {
            Comparer = comparer;
        }

        private SynonymCollection()
        {
            Items = new List<KeyStringPair>();
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

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

        private List<KeyStringPair> Items { get; set; }

        public void Add(string value)
        {
            Items.Add(new KeyStringPair(value, Comparer.Normalize(value)));
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(string value)
        {
#if NET20
            foreach (var item in Items)
            {
                if (0 == Comparer.Compare(item.Value, value))
                {
                    return true;
                }
            }

            return false;
#else
            return Items.Any(item => 0 == Comparer.Compare(item.Value, value));
#endif
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<string> obj = this;

            return obj.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
#if NET20
            foreach (var item in Items)
            {
                yield return item.Key;
            }
#else
            return Items.Select(item => item.Key).GetEnumerator();
#endif
        }
    }
}