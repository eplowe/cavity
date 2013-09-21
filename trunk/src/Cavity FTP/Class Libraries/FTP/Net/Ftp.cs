namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Cavity;

    [XmlRoot("ftp")]
    public sealed class Ftp : IXmlSerializable
    {
        public ICredentials Credentials { get; private set; }

        public IEnumerable<FtpFile> Files
        {
            get
            {
                return FtpDirectory.Load(Uri, Credentials, Passive, Secure);
            }
        }

        public bool Passive { get; set; }

        public bool Secure { get; set; }

        public AbsoluteUri Uri { get; private set; }

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

            Uri = reader.GetAttribute("uri");
            Credentials = new NetworkCredential(reader.GetAttribute("user"), reader.GetAttribute("password"));
            Passive = XmlConvert.ToBoolean(reader.GetAttribute("passive") ?? "true");
            Secure = XmlConvert.ToBoolean(reader.GetAttribute("secure") ?? "false");
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}
