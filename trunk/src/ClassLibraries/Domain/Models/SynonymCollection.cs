namespace Cavity.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class SynonymCollection : IEnumerable<string>
    {
        private ILexiconComparer _comparer;

        public SynonymCollection(ILexiconComparer comparer)
            : this()
        {
            Comparer = comparer;
        }

        private SynonymCollection()
        {
            Items = new List<KeyValuePair<string, string>>();
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        private ILexiconComparer Comparer
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

        private List<KeyValuePair<string, string>> Items { get; set; }

        public void Add(string value)
        {
            Items.Add(new KeyValuePair<string, string>(value, Comparer.Normalize(value)));
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(string value)
        {
            return Items.Any(item => 0 == Comparer.Compare(item.Value, value));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable<string> obj = this;

            return obj.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return Items.Select(item => item.Key).GetEnumerator();
        }
    }
}