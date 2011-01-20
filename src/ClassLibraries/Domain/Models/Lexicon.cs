namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cavity.Collections.Generic;
    using Cavity.Data;

    public sealed class Lexicon
    {
        private readonly List<LexicalItem> _items;

        private INormalizationComparer _comparer;

        public Lexicon(INormalizationComparer comparer)
            : this()
        {
            Comparer = comparer;
            _items = new List<LexicalItem>();
        }

        private Lexicon()
        {
        }

        public IEnumerable<LexicalItem> Items
        {
            get
            {
                foreach (var item in _items)
                {
                    yield return item;
                }
            }
        }

        public IStoreLexicon Storage { get; set; }

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

        public LexicalItem this[string spelling]
        {
            get
            {
                if (null == spelling)
                {
                    throw new ArgumentNullException("spelling");
                }

                return Items.Where(x => x.Contains(spelling)).FirstOrDefault();
            }
        }

        public LexicalItem Add(string value)
        {
            var item = this[value];
            if (null != item)
            {
                return item;
            }

            item = new LexicalItem(Comparer, value);

            _items.Add(item);

            return item;
        }

        public bool Contains(string value)
        {
            return Items.Any(item => item.Contains(value));
        }

        public void Delete()
        {
            Delete(Storage);
        }

        public void Delete(IStoreLexicon storage)
        {
            if (null == storage)
            {
                throw new ArgumentNullException("storage");
            }

            Storage = storage;
            Storage.Delete(this);
        }

        public void Invoke(Func<string, string> function)
        {
            if (null == function)
            {
                throw new ArgumentNullException("function");
            }

            foreach (var item in Items)
            {
                item.Invoke(function);
            }
        }

        public void Remove(IEnumerable<LexicalItem> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            if (0 == items.Count())
            {
                return;
            }

            foreach (var item in items.ToList())
            {
                foreach (var spelling in item.Spellings)
                {
                    var match = this[spelling];
                    if (null == match)
                    {
                        continue;
                    }

                    _items.Remove(match);
                }
            }
        }

        public void Save()
        {
            Save(Storage);
        }

        public void Save(IStoreLexicon storage)
        {
            if (null == storage)
            {
                throw new ArgumentNullException("storage");
            }

            Storage = storage;
            Storage.Save(this);
        }

        public string ToCanonicalForm(string value)
        {
            return (from item in Items
                    where item.Contains(value)
                    select item.CanonicalForm).FirstOrDefault();
        }
    }
}