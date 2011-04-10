namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.Data;

    public sealed class RecordFile
    {
        public RecordFile()
        {
            Headers = new Dictionary<string, string>();
        }

        public RecordFile(IRecord record)
            : this()
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            Headers["urn"] = record.Urn;
            Headers["key"] = record.Key.HasValue ? record.Key.Value : string.Empty;
            Headers["etag"] = record.Etag;
            Headers["created"] = record.Created.ToXmlString();
            Headers["modified"] = record.Modified.ToXmlString();
            Headers["cacheability"] = record.Cacheability;
            Headers["expiration"] = record.Expiration;
            Headers["status"] = record.Status.HasValue ? record.Status.Value.ToXmlString() : string.Empty;
            var xml = record.ToXml();
            if (null == xml)
            {
                return;
            }

            Body = xml.CreateNavigator().OuterXml;
        }
        
        public string Body { get; set; }

        public IDictionary<string, string> Headers { get; private set; }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (var header in Headers)
            {
                buffer.AppendLine("{0}: {1}".FormatWith(header.Key, header.Value));
            }

            buffer.AppendLine(string.Empty);
            if (null != Body)
            {
                buffer.Append(Body);
            }

            return buffer.ToString();
        }

        public IXPathNavigable ToXml()
        {
            if (null == Body)
            {
                return null;
            }

            var xml = new XmlDocument();
            xml.LoadXml(Body);

            return xml;
        }
    }
}