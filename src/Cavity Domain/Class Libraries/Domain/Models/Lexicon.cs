namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Collections;
    using Cavity.Data;

    public class Lexicon
    {
        private readonly List<LexicalItem> _items;

        private INormalizationComparer _comparer;

        public Lexicon(INormalizationComparer comparer)
            : this()
        {
            Comparer = comparer;
        }

        private Lexicon()
        {
            _items = new List<LexicalItem>();
        }

        public IEnumerable<LexicalItem> Items
        {
            get
            {
                return _items;
            }
        }

        public IStoreLexicon Storage { get; set; }

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

        public virtual LexicalItem this[string spelling]
        {
            get
            {
                if (null == spelling)
                {
                    throw new ArgumentNullException("spelling");
                }

#if NET20
                foreach (var item in Items)
                {
                    if (item.Contains(spelling))
                    {
                        return item;
                    }
                }

                return null;
#else
                return Items.Where(item => item.Contains(spelling)).FirstOrDefault();
#endif
            }
        }
        
        public virtual LexicalItem Add(LexicalItem item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            var copy = Add(item.CanonicalForm);
            foreach (var synonym in item.Synonyms)
            {
                copy.Synonyms.Add(synonym);
            }

            return copy;
        }

        public virtual LexicalItem Add(string value)
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

        public virtual bool Contains(string value)
        {
#if NET20
            foreach (var item in Items)
            {
                if (item.Contains(value))
                {
                    return true;
                }
            }

            return false;
#else
            return Items.Any(item => item.Contains(value));
#endif
        }

        public virtual void Delete()
        {
            Delete(Storage);
        }

        public virtual void Delete(IStoreLexicon storage)
        {
            if (null == storage)
            {
                throw new ArgumentNullException("storage");
            }

            Storage = storage;
            Storage.Delete(this);
        }

#if !NET20
        public virtual void Invoke(Func<string, string> func)
        {
            if (null == func)
            {
                throw new ArgumentNullException("func");
            }

            foreach (var item in Items)
            {
                item.Invoke(func);
            }
        }
#endif

        public virtual void MoveTo(Lexicon destination, LexicalItem item)
        {
            if (null == destination)
            {
                throw new ArgumentNullException("destination");
            }

            if (ReferenceEquals(this, destination))
            {
                throw new InvalidOperationException("The source and destination cannot be the same instance.");
            }

            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            Remove(new[] { item });
            destination.Add(item);
        }

        public virtual void Remove(IEnumerable<LexicalItem> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

#if NET20
            foreach (var item in items)
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
#else
            foreach (var match in items.SelectMany(item => item.Spellings
                                                               .Select(x => this[x])
                                                               .Where(match => null != match)))
            {
                _items.Remove(match);
            }
#endif
        }

        public virtual void Save()
        {
            Save(Storage);
        }

        public virtual void Save(IStoreLexicon storage)
        {
            if (null == storage)
            {
                throw new ArgumentNullException("storage");
            }

            Storage = storage;
            Storage.Save(this);
        }

        public virtual string ToCanonicalForm(string value)
        {
#if NET20
            foreach (var item in Items)
            {
                if (!item.Contains(value))
                {
                    continue;
                }

                return item.CanonicalForm;
            }

            return null;
#else
            return (from item in Items
                    where item.Contains(value)
                    select item.CanonicalForm).FirstOrDefault();
#endif
        }
    }
}