namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Cavity.Data;

    public sealed class Lexicon
    {
        public Lexicon()
            : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public Lexicon(IComparer<string> comparer)
        {
            Items = new Collection<LexicalItem>();
            Comparer = comparer;
        }

        public IComparer<string> Comparer { get; set; }

        public ICollection<LexicalItem> Items { get; private set; }

        public IStoreLexicon Storage { get; set; }

        public LexicalItem this[string spelling]
        {
            get
            {
                if (null == spelling)
                {
                    throw new ArgumentNullException("spelling");
                }

                return Items.Where(x => x.Contains(spelling, Comparer)).FirstOrDefault();
            }
        }

        public void Add(string value)
        {
            if (Contains(value))
            {
                return;
            }

            Items.Add(new LexicalItem(value));
        }

        public bool Contains(string value)
        {
            return Items.Any(item => item.Contains(value, Comparer));
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

                    Items.Remove(match);
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
                    where item.Contains(value, Comparer)
                    select item.CanonicalForm).FirstOrDefault();
        }
    }
}