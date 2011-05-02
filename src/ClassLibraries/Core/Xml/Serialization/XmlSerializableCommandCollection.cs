namespace Cavity.Xml.Serialization
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlRoot("commands")]
    public sealed class XmlSerializableCommandCollection : Collection<IXmlSerializableCommand>, IXmlSerializable
    {
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

                while ("command".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var attribute = reader.GetAttribute("type");
                    if (string.IsNullOrEmpty(attribute))
                    {
                        throw new InvalidOperationException();
                    }

                    Add((IXmlSerializableCommand)reader.ReadInnerXml().XmlDeserialize(Type.GetType(attribute)));
                }
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var item in Items)
            {
                writer.WriteStartElement("command");
                writer.WriteAttributeString("type", item.GetType().AssemblyQualifiedName);
                writer.WriteRaw(item.XmlSerialize().CreateNavigator().OuterXml);
                writer.WriteEndElement();
            }
        }
    }
}