namespace Cavity.Types
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public sealed class XmlSerializationClass3 : IXmlSerializable
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
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
        }
    }
}