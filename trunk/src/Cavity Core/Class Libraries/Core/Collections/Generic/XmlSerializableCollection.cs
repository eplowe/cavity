namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.ObjectModel;
#if !NET20
    using System.Linq;
#endif
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Cavity.Xml;

    public abstract class XmlSerializableCollection<T> : Collection<T>,
                                                         IEquatable<XmlSerializableCollection<T>>,
                                                         IXmlSerializable
        where T : IEquatable<T>, new()
    {
        public override bool Equals(object obj)
        {
            return Equals((XmlSerializableCollection<T>)obj);
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

        public virtual bool Equals(XmlSerializableCollection<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Count != other.Count)
            {
                return false;
            }

#if NET20
            foreach (var item in other)
            {
                if (Contains(item))
                {
                    continue;
                }

                return false;
            }

            return true;
#else
            return this.All(other.Contains);
#endif
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

                while (XmlNodeType.Element == reader.NodeType)
                {
#if NET20
                    Add(XmlReaderExtensionMethods.Deserialize<T>(reader));
#else
                    Add(reader.Deserialize<T>());
#endif
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var item in this)
            {
#if NET20
                writer.WriteRaw(ObjectExtensionMethods.XmlSerialize(item).CreateNavigator().OuterXml);
#else
                writer.WriteRaw(item.XmlSerialize().CreateNavigator().OuterXml);
#endif
            }
        }
    }
}