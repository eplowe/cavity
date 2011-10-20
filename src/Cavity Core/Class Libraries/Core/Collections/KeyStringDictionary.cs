namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Cavity.Data;
    using Cavity.Properties;

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

        public string this[int index]
        {
            get
            {
                if (index > -1 && index < Count)
                {
                    var i = 0;
                    foreach (var key in Keys)
                    {
                        if (i == index)
                        {
                            return this[key];
                        }

                        i++;
                    }
                }

                throw new ArgumentOutOfRangeException("index", Resources.IndexOutOfRangeException_Message);
            }
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

        public virtual T Value<T>(int index)
        {
#if NET20
            return StringExtensionMethods.To<T>(this[index]);
#else
            return this[index].To<T>();
#endif
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