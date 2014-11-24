namespace Cavity.Configuration
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Cavity.Collections;
    using Cavity.Xml;

    [XmlRoot("paths")]
    public sealed class Paths : IXmlSerializable
    {
        public DirectoryCollection Directories { get; private set; }

        public FileCollection Files { get; private set; }

        public DirectoryInfo Directory(string name)
        {
            return Directories[name];
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
#if NET20
                if (XmlReaderExtensionMethods.IsEndElement(reader, name))
#else
                if (reader.IsEndElement(name))
#endif
                {
                    reader.Read();
                    break;
                }

                while (XmlNodeType.Element == reader.NodeType)
                {
                    switch (reader.Name)
                    {
                        case "directories":
#if NET20
                            Directories = XmlReaderExtensionMethods.Deserialize<DirectoryCollection>(reader);
#else
                            Directories = reader.Deserialize<DirectoryCollection>();
#endif
                            break;
                        case "files":
#if NET20
                            Files = XmlReaderExtensionMethods.Deserialize<FileCollection>(reader);
#else
                            Files = reader.Deserialize<FileCollection>();
#endif
                            break;
                    }
                }
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }
        }
    }
}