namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Data;

    public class SynonymCollection : IEnumerable<string>
    {
        private INormalizationComparer _comparer;

        public SynonymCollection(INormalizationComparer comparer)
            : this()
        {
            Comparer = comparer;
        }

        private SynonymCollection()
        {
            Items = new Collection<KeyStringPair>();
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

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

        protected Collection<KeyStringPair> Items { get; private set; }

        public virtual void Add(string value)
        {
            Items.Add(new KeyStringPair(value, Comparer.Normalize(value)));
        }

        public virtual void Clear()
        {
            Items.Clear();
        }

        public virtual bool Contains(string value)
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
            return GetEnumerator();
        }

        public virtual IEnumerator<string> GetEnumerator()
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