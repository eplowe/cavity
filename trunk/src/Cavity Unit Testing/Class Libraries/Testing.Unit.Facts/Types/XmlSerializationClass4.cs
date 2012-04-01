namespace Cavity.Types
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public sealed class XmlSerializationClass4 : IXmlSerializable
    {
        public override bool Equals(object obj)
        {
            var value = obj as XmlSerializationClass4;

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