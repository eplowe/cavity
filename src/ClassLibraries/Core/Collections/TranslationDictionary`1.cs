namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Cavity.Globalization;

    [Serializable]
    public class TranslationDictionary<T> : Dictionary<Language, T>, IEnumerable<Translation<T>>
    {
        public TranslationDictionary()
        {
        }

        protected TranslationDictionary(SerializationInfo info,
                                        StreamingContext context)
            : base(info, context)
        {
        }

        public virtual void Add(Translation<T> item)
        {
            Add(item.Language, item.Value);
        }

        public virtual bool Contains(Translation<T> item)
        {
            return (this as IDictionary<Language, T>).Contains(new KeyValuePair<Language, T>(item.Language, item.Value));
        }

        public virtual bool Remove(Translation<T> item)
        {
            return (this as IDictionary<Language, T>).Remove(new KeyValuePair<Language, T>(item.Language, item.Value));
        }

        public new IEnumerator<Translation<T>> GetEnumerator()
        {
            var e = base.GetEnumerator();
            while (e.MoveNext())
            {
                yield return new Translation<T>(e.Current.Value, e.Current.Key);
            }
        }
    }
}