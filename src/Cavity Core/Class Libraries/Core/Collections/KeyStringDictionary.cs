namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization;

    using Cavity.Data;
    using Cavity.Properties;

    [Serializable]
    public class KeyStringDictionary : Dictionary<string, string>,
                                       IEnumerable<KeyStringPair>,
                                       ICloneable
    {
        public KeyStringDictionary()
            : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public KeyStringDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        protected KeyStringDictionary(SerializationInfo info,
                                      StreamingContext context)
            : base(info, context)
        {
        }

        public new string this[string key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch (KeyNotFoundException)
                {
                    throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "The '{0}' key was not present in the dictionary.", key));
                }
            }
            
            set
            {
                base[key] = value;
            }
        }

        public void Set(string key,
                        string value)
        {
            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "The '{0}' key was not present in the dictionary.", key));
            }

            this[key] = value;
        }

        public string this[int index]
        {
            get
            {
                if (index > -1 &&
                    index < Count)
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

        public virtual object Clone()
        {
            return Clone<KeyStringDictionary>();
        }

        public virtual T Clone<T>()
            where T : KeyStringDictionary, new()
        {
            var clone = Activator.CreateInstance<T>();

            CopyTo(clone);

            return clone;
        }

        public virtual bool Contains(KeyStringPair item)
        {
            return (this as IDictionary<string, string>).Contains(new KeyValuePair<string, string>(item.Key, item.Value));
        }

        public virtual void CopyTo(KeyStringDictionary target)
        {
            if (null == target)
            {
                throw new ArgumentNullException("target");
            }

            foreach (var item in this)
            {
                target[item.Key] = item.Value;
            }
        }

        public virtual bool Empty(params string[] keys)
        {
            if (null == keys)
            {
                throw new ArgumentNullException("keys");
            }
#if NET20
            if (0 == keys.Length)
            {
                keys = ToKeyArray();
            }

            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(this[key]))
                {
                    continue;
                }

                return false;
            }

            return true;
#else
            return 0 == keys.Length
                       ? Empty(Keys.ToArray())
                       : keys.All(key => string.IsNullOrEmpty(this[key]));
#endif
        }

        public virtual int Length(params string[] keys)
        {
            if (null == keys)
            {
                throw new ArgumentNullException("keys");
            }
#if NET20
            if (0 == keys.Length)
            {
                keys = ToKeyArray();
            }

            var sum = 0;
            foreach (var key in keys)
            {
                sum += this[key].Length;
            }

            return sum;
#else
            return 0 == keys.Length
                       ? Length(Keys.ToArray())
                       : keys.Sum(key => this[key].Length);
#endif
        }

        public virtual void Move(string source,
                                 string destination)
        {
            this[destination] = this[source];
            this[source] = string.Empty;
        }

        public virtual bool NotContains(KeyStringPair item)
        {
            return !Contains(item);
        }

        public virtual bool NotEmpty(params string[] keys)
        {
            return !Empty(keys);
        }

        public virtual bool Remove(KeyStringPair item)
        {
            return (this as IDictionary<string, string>).Remove(new KeyValuePair<string, string>(item.Key, item.Value));
        }

        public virtual void RemoveAny(params string[] keys)
        {
            if (null == keys)
            {
                throw new ArgumentNullException("keys");
            }

            if (0 == keys.Length)
            {
                throw new ArgumentOutOfRangeException("keys");
            }

            foreach (var key in keys)
            {
                if (!ContainsKey(key))
                {
                    continue;
                }

                Remove(key);
            }
        }

        public virtual IEnumerable<string> Strings(params string[] keys)
        {
            if (null == keys)
            {
                throw new ArgumentNullException("keys");
            }
#if NET20
            if (0 == keys.Length)
            {
                keys = ToKeyArray();
            }

            foreach (var key in keys)
            {
                yield return this[key];
            }
#else
            return 0 == keys.Length
                       ? Strings(Keys.ToArray())
                       : keys.Select(key => this[key]);
#endif
        }

        public virtual T TryValue<T>(int index)
        {
#if NET20
            return StringExtensionMethods.TryTo<T>(this[index]);
#else
            return this[index].TryTo<T>();
#endif
        }

        public virtual T TryValue<T>(int index,
                                     T empty)
        {
            var value = this[index];

#if NET20 || NET35
            return StringExtensionMethods.IsNullOrWhiteSpace(value)
#else
            return string.IsNullOrWhiteSpace(value)
#endif
                       ? empty
                       : TryValue<T>(index);
        }

        public virtual T TryValue<T>(string key)
        {
#if NET20
            return StringExtensionMethods.TryTo<T>(this[key]);
#else
            return this[key].TryTo<T>();
#endif
        }

        public virtual T TryValue<T>(string key,
                                     T empty)
        {
            var value = this[key];

#if NET20 || NET35
            return StringExtensionMethods.IsNullOrWhiteSpace(value)
#else
            return string.IsNullOrWhiteSpace(value)
#endif
                       ? empty
                       : TryValue<T>(key);
        }

        public virtual T Value<T>(int index)
        {
#if NET20
            return StringExtensionMethods.To<T>(this[index]);
#else
            return this[index].To<T>();
#endif
        }

        public virtual T Value<T>(int index,
                                  T empty)
        {
            var value = this[index];

#if NET20 || NET35
            return StringExtensionMethods.IsNullOrWhiteSpace(value)
#else
            return string.IsNullOrWhiteSpace(value)
#endif
                       ? empty
                       : Value<T>(index);
        }

        public virtual T Value<T>(string key)
        {
#if NET20
            return StringExtensionMethods.To<T>(this[key]);
#else
            return this[key].To<T>();
#endif
        }

        public virtual T Value<T>(string key,
                                  T empty)
        {
            var value = this[key];

#if NET20 || NET35
            return StringExtensionMethods.IsNullOrWhiteSpace(value)
#else
            return string.IsNullOrWhiteSpace(value)
#endif
                       ? empty
                       : Value<T>(key);
        }

        public new IEnumerator<KeyStringPair> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return new KeyStringPair(e.Current.Key, e.Current.Value);
            }
        }

#if NET20
        private string[] ToKeyArray()
        {
            var keys = new string[Keys.Count];
            var i = 0;
            foreach (var key in Keys)
            {
                keys[i++] = key;
            }

            return keys;
        }
#endif
    }
}