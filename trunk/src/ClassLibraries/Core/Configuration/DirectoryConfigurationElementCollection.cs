namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public sealed class DirectoryConfigurationElementCollection : ConfigurationElementCollection, ICollection<DirectoryConfigurationElement>
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

        protected override string ElementName
        {
            get
            {
                return "directories";
            }
        }

        public bool Contains(string path)
        {
            return null != path && this.Any(pickup => pickup.Directory.FullName.Equals(path));
        }

        public void Add(DirectoryConfigurationElement item)
        {
            BaseAdd(item);
        }

        public void Clear()
        {
            BaseClear();
        }

        public bool Contains(DirectoryConfigurationElement item)
        {
            return null != item && this.Any(pickup => ReferenceEquals(pickup, item));
        }

        public void CopyTo(DirectoryConfigurationElement[] array,
                           int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public bool Remove(DirectoryConfigurationElement item)
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

        public new IEnumerator<DirectoryConfigurationElement> GetEnumerator()
        {
            var list = new List<DirectoryConfigurationElement>();
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current as DirectoryConfigurationElement;
                list.Add(item);
            }

            return list.GetEnumerator();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            var extension = element as DirectoryConfigurationElement;
            if (null == extension)
            {
                throw new ArgumentOutOfRangeException("element");
            }

            return extension.Directory.FullName;
        }
    }
}