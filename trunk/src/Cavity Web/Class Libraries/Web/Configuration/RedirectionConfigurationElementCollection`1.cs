namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public sealed class RedirectionConfigurationElementCollection<T> : ConfigurationElementCollection, ICollection<RedirectionConfigurationElement<T>>
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public new bool IsReadOnly
        {
            get
            {
                return IsReadOnly();
            }
        }

        public void Add(T from,
                        T to)
        {
            BaseAdd(new RedirectionConfigurationElement<T>(from, to));
        }

        public void Add(RedirectionConfigurationElement<T> item)
        {
            BaseAdd(item);
        }

        public void Clear()
        {
            BaseClear();
        }

        public bool Contains(RedirectionConfigurationElement<T> item)
        {
#if NET20
            foreach (var element in this)
            {
                if (ReferenceEquals(element, item))
                {
                    return true;
                }
            }

            return false;
#else
            return this.Any(element => ReferenceEquals(element, item));
#endif
        }

        public void CopyTo(RedirectionConfigurationElement<T>[] array,
                           int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public bool Remove(RedirectionConfigurationElement<T> item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (!ReferenceEquals(BaseGet(i), item))
                {
                    continue;
                }

                BaseRemoveAt(i);
                return true;
            }

            return false;
        }

        public new IEnumerator<RedirectionConfigurationElement<T>> GetEnumerator()
        {
            var list = new List<RedirectionConfigurationElement<T>>();
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current as RedirectionConfigurationElement<T>;
                list.Add(item);
            }

            return list.GetEnumerator();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RedirectionConfigurationElement<T>();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            var extension = element as RedirectionConfigurationElement<T>;
            if (null == extension)
            {
                throw new ArgumentOutOfRangeException("element");
            }

            return extension.From;
        }
    }
}