namespace Cavity.Xml
{
    using System;
    using System.Xml;

    public static class XmlReaderExtensionMethods
    {
#if NET20
        public static T Deserialize<T>(XmlReader reader)
#else
        public static T Deserialize<T>(this XmlReader reader)
#endif
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }
#if NET20
            return StringExtensionMethods.XmlDeserialize<T>(reader.ReadOuterXml());
#else
            return reader.ReadOuterXml().XmlDeserialize<T>();
#endif
        }

#if NET20
        public static bool IsEndElement(XmlReader reader,
                                        string name)
#else
        public static bool IsEndElement(this XmlReader reader,
                                        string name)
#endif
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (XmlNodeType.EndElement != reader.NodeType)
            {
                return false;
            }

            return reader.Name == name;
        }
    }
}