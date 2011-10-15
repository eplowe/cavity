namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Cavity.Data;

    [Serializable]
    public class KeyStringDictionary : Dictionary<string, string>, IEnumerable<KeyStringPair>
    {
        public KeyStringDictionary()
        {
        }

        protected KeyStringDictionary(SerializationInfo info,
                                      StreamingContext context)
            : base(info, context)
        {
        }

        public virtual void Add(KeyStringPair item)
        {
            Add(item.Key, item.Value);
        }

        public virtual bool Contains(KeyStringPair item)
        {
            return (this as IDictionary<string, string>).Contains(new KeyValuePair<string, string>(item.Key, item.Value));
        }

        public virtual bool Remove(KeyStringPair item)
        {
            return (this as IDictionary<string, string>).Remove(new KeyValuePair<string, string>(item.Key, item.Value));
        }

        public new IEnumerator<KeyStringPair> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return new KeyStringPair(e.Current.Key, e.Current.Value);
            }
        }

        public virtual T Value<T>(string key)
        {
#if NET20
            return StringExtensionMethods.To<T>(this[key]);
#else
            return this[key].To<T>();
#endif
        }
    }
}