namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlRoot("data")]
    public sealed class DataCollection : IEnumerable<KeyStringPair>, IXmlSerializable
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

        public KeyStringPair this[int index]
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

        public string this[string name]
        {
            get
            {
                StringBuilder buffer = null;

                foreach (var datum in this.Where(datum => 0 == string.CompareOrdinal(name, datum.Key)))
                {
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

                foreach (var datum in Items.Where(datum => 0 == string.CompareOrdinal(name, datum.Key)))
                {
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

                foreach (var part in value.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    result.Add(form.Keys[i], part);
                }
            }

            return result;
        }

        public void Add(DataCollection data)
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

        public void Add(string name,
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

        public void Add(KeyStringPair item)
        {
            if (null == item.Value)
            {
                Items.Add(new KeyStringPair(item.Key, item.Value));
                return;
            }

            foreach (var part in item.Value.Split(',', StringSplitOptions.None))
            {
                Items.Add(new KeyStringPair(item.Key, part));
            }
        }

        public bool Contains(string name)
        {
            return 0 != Items.Where(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
                            .Count();
        }

        public bool Contains(string name,
                             string value)
        {
            return 0 != Items.Where(x => x.Key.Equals(name, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.Ordinal))
                            .Count();
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

            return Items.Count == cast.Items.Count
                       ? Items.All(datum => cast.Items.Contains(datum))
                       : false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.XmlSerialize().CreateNavigator().OuterXml;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Items as IEnumerable).GetEnumerator();
        }

        IEnumerator<KeyStringPair> IEnumerable<KeyStringPair>.GetEnumerator()
        {
            return (Items as IEnumerable<KeyStringPair>).GetEnumerator();
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotSupportedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
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

        void IXmlSerializable.WriteXml(XmlWriter writer)
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