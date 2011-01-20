namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MultitonCollection<TKey, TValue> : IEnumerable<TKey>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        public MultitonCollection()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Add(key, Activator.CreateInstance<TValue>());
                }

                return GetValue(key);
            }

            set
            {
                if (!ContainsKey(key))
                {
                    Add(key, value);
                }
                else
                {
                    _dictionary[key] = value;
                }
            }
        }

        public virtual void Add(TKey key,
                                TValue value)
        {
            _dictionary[key] = value;
        }

        public virtual bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public virtual TValue GetValue(TKey key)
        {
            return _dictionary[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_dictionary.Keys as IEnumerable).GetEnumerator();
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return _dictionary.Keys.GetEnumerator();
        }
    }
}