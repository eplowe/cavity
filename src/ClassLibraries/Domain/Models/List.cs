namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    [XmlRoot("list")]
    public sealed class List : List<string>, IXmlSerializable
    {
        public IEnumerable<T> To<T>()
        {
            foreach (var item in this)
            {
                yield return (T)Convert.ChangeType(item, typeof(T));
            }
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

                if ("item".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Add(reader.ReadString());
                }
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var item in this)
            {
                writer.WriteStartElement("item");
                writer.WriteString(item);
                writer.WriteEndElement();
            }
        }
    }
}