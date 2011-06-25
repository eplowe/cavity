namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlRoot("data")]
    public class DataCollection : IEnumerable<KeyStringPair>, IXmlSerializable
    {
        public DataCollection()
        {
            Items = new Collection<KeyStringPair>();
        }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        private Collection<KeyStringPair> Items { get; set; }

        public virtual KeyStringPair this[int index]
        {
            get
            {
                return Items[index];
            }

            set
            {
                Items[index] = value;
            }
        }

        public virtual string this[string name]
        {
            get
            {
                StringBuilder buffer = null;

#if NET20
                foreach (var datum in this)
#else
                foreach (var datum in this.Where(datum => 0 == string.CompareOrdinal(name, datum.Key)))
#endif
                {
#if NET20
                    if (0 != string.CompareOrdinal(name, datum.Key))
                    {
                        continue;
                    }
#endif

                    if (null == buffer)
                    {
                        buffer = new StringBuilder();
                    }

                    if (0 != buffer.Length)
                    {
                        buffer.Append(',');
                    }

                    buffer.Append(datum.Value);
                }

                return null == buffer ? null : buffer.ToString();
            }

            set
            {
                var removals = new Collection<KeyStringPair>();

#if NET20
                foreach (var datum in Items)
#else
                foreach (var datum in Items.Where(datum => 0 == string.CompareOrdinal(name, datum.Key)))
#endif
                {
#if NET20
                    if (0 != string.CompareOrdinal(name, datum.Key))
                    {
                        continue;
                    }
#endif

                    removals.Add(datum);
                }

                foreach (var removal in removals)
                {
                    Items.Remove(removal);
                }

                if (null == value)
                {
                    Items.Add(new KeyStringPair(name, value));
                    return;
                }

                foreach (var part in value.Split(','))
                {
                    Items.Add(new KeyStringPair(name, part));
                }
            }
        }

        public static bool operator ==(DataCollection obj,
                                       DataCollection comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static bool operator !=(DataCollection obj,
                                       DataCollection comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public static DataCollection FromPostData(NameValueCollection form)
        {
            if (null == form)
            {
                throw new ArgumentNullException("form");
            }

            var result = new DataCollection();

            for (var i = 0; i < form.Count; i++)
            {
                var value = form[i];
                if (null == value)
                {
                    result.Add(form.Keys[i], form[i]);
                    continue;
                }

                if (!value.Contains(","))
                {
                    result.Add(form.Keys[i], form[i]);
                    continue;
                }

#if NET20
                foreach (var part in StringExtensionMethods.Split(value, ',', StringSplitOptions.RemoveEmptyEntries))
#else
                foreach (var part in value.Split(',', StringSplitOptions.RemoveEmptyEntries))
#endif
                {
                    result.Add(form.Keys[i], part);
                }
            }

            return result;
        }

        public virtual void Add(DataCollection data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            foreach (var datum in data.Items)
            {
                Items.Add(datum);
            }
        }

        public virtual void Add(string name,
                                string value)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            Add(new KeyStringPair(name, value));
        }

        public virtual void Add(KeyStringPair item)
        {
            if (null == item.Value)
            {
                Items.Add(new KeyStringPair(item.Key, item.Value));
                return;
            }

#if NET20
            foreach (var part in StringExtensionMethods.Split(item.Value, ',', StringSplitOptions.None))
#else
            foreach (var part in item.Value.Split(',', StringSplitOptions.None))
#endif
            {
                Items.Add(new KeyStringPair(item.Key, part));
            }
        }

        public virtual bool Contains(string name)
        {
#if NET20
            foreach (var item in Items)
            {
                if (item.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
#else
            return 0 != Items.Where(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
                            .Count();
#endif
        }

        public virtual bool Contains(string name,
                                     string value)
        {
#if NET20
            foreach (var item in Items)
            {
                if (item.Key.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                    item.Value.Equals(value, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
#else
            return 0 != Items.Where(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.Ordinal))
                            .Count();
#endif
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var cast = obj as DataCollection;
            if (ReferenceEquals(null, cast))
            {
                return false;
            }

#if NET20
            if (Items.Count != cast.Items.Count)
            {
                return false;
            }

            foreach (var item in Items)
            {
                if (!cast.Items.Contains(item))
                {
                    return false;
                }
            }

            return true;
#else
            return Items.Count == cast.Items.Count
                       ? Items.All(datum => cast.Items.Contains(datum))
                       : false;
#endif
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
#if NET20
            return ObjectExtensionMethods.XmlSerialize(this).CreateNavigator().OuterXml;
#else
            return this.XmlSerialize().CreateNavigator().OuterXml;
#endif
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerator<KeyStringPair> GetEnumerator()
        {
            return (Items as IEnumerable<KeyStringPair>).GetEnumerator();
        }

        public virtual XmlSchema GetSchema()
        {
            throw new NotSupportedException();
        }

        public virtual void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }

            var name = reader.Name;
            while (reader.Read())
            {
                if (XmlNodeType.EndElement == reader.NodeType &&
                    reader.Name == name)
                {
                    reader.Read();
                    break;
                }

                if ("value".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Add(reader.GetAttribute("name"), reader.ReadString());
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var datum in Items)
            {
                writer.WriteStartElement("value");
                writer.WriteAttributeString("name", datum.Key);
                writer.WriteString(datum.Value);
                writer.WriteEndElement();
            }
        }
    }
}