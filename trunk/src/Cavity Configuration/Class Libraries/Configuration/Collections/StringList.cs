namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    [XmlRoot("list")]
    public class StringList : List<string>,
                              IXmlSerializable
    {
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

            var cast = obj as StringList;

            return !ReferenceEquals(null, cast)
                   && string.Equals(ToString(), cast.ToString(), StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public virtual IEnumerable<T> ToEnumerable<T>()
        {
#if NET20
            foreach (var item in this)
            {
                yield return (T)Convert.ChangeType(item, typeof(T), CultureInfo.InvariantCulture);
            }
#else
            return this.Select(item => (T)Convert.ChangeType(item, typeof(T), CultureInfo.InvariantCulture));
#endif
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
#if NET20 || NET35
            foreach (var item in this)
            {
                buffer.AppendLine(item);
            }
#else
            Serial.ForEach(this, item => buffer.AppendLine(item));
#endif
            return buffer.ToString();
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

                if ("item".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Add(reader.ReadString());
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
                writer.WriteStartElement("item");
                writer.WriteString(item);
                writer.WriteEndElement();
            }
        }
    }
}