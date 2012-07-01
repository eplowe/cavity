namespace Cavity.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Threading;

    [Serializable]
    public class TranslationDictionary<T> : Dictionary<Language, T>, 
                                            IEnumerable<Translation<T>>
    {
        public TranslationDictionary()
        {
        }

        protected TranslationDictionary(SerializationInfo info, 
                                        StreamingContext context)
            : base(info, context)
        {
        }

        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Throwing this exception type is intentional.")]
        public T Current
        {
            get
            {
                var culture = Thread.CurrentThread.CurrentUICulture;
                if (ContainsKey(culture))
                {
                    return this[culture];
                }

                var name = culture.TwoLetterISOLanguageName;
                if (ContainsKey(name))
                {
                    return this[name];
                }

                name = CultureInfo.InvariantCulture.Name;
                if (ContainsKey(name))
                {
                    return this[name];
                }

                throw new TranslationException(string.Format(CultureInfo.InvariantCulture, "A translation for {0} could not be found.", culture));
            }
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