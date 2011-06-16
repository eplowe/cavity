namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public sealed class FileConfigurationElementCollection : ConfigurationElementCollection, ICollection<FileConfigurationElement>
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
                return "files";
            }
        }

        public bool Contains(string path)
        {
            foreach (var item in this)
            {
                if (item.File.FullName.Equals(path))
                {
                    return true;
                }
            }

            return false;
        }

        public void Add(FileConfigurationElement item)
        {
            BaseAdd(item);
        }

        public void Clear()
        {
            BaseClear();
        }

        public bool Contains(FileConfigurationElement item)
        {
            foreach (var element in this)
            {
                if (ReferenceEquals(element, item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(FileConfigurationElement[] array,
                           int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public bool Remove(FileConfigurationElement item)
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

        public new IEnumerator<FileConfigurationElement> GetEnumerator()
        {
            var list = new List<FileConfigurationElement>();
            var enumerator = base.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current as FileConfigurationElement;
                list.Add(item);
            }

            return list.GetEnumerator();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FileConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            var extension = element as FileConfigurationElement;
            if (null == extension)
            {
                throw new ArgumentOutOfRangeException("element");
            }

            return extension.Name;
        }
    }
}