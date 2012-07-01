namespace Cavity.Types
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [XmlRoot("example")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a test class.")]
    public sealed class XmlSerializationClass6 : Collection<string>,
                                                 IXmlSerializable
    {
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