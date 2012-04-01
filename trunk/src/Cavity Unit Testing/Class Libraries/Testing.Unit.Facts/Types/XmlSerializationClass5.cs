namespace Cavity.Types
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlRoot("example")]
    public sealed class XmlSerializationClass5 : IXmlSerializable
    {
        public override bool Equals(object obj)
        {
            var value = obj as XmlSerializationClass5;

            return null != value;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public XmlSchema GetSchema()
        {
            throw new NotSupportedException();
        }

        public void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }
        }
    }
}