namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.Data;

    public sealed class RecordFile
    {
        public RecordFile()
        {
            Headers = new Dictionary<string, string>
            {
                {
                    "urn", string.Empty
                    },
                {
                    "key", string.Empty
                    },
                {
                    "etag", string.Empty
                    },
                {
                    "created", string.Empty
                    },
                {
                    "modified", string.Empty
                    },
                {
                    "cacheability", string.Empty
                    },
                {
                    "expiration", string.Empty
                    },
                {
                    "status", string.Empty
                    }
            };
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

        public FileSystemInfo Location { get; private set; }

        public static RecordFile Load(FileSystemInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            var result = new RecordFile();
            using (var stream = new FileInfo(file.FullName)
                .Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (true)
                    {
                        var line = reader.ReadLine();
                        if (null == line)
                        {
                            break;
                        }

                        if (0 == line.Length)
                        {
                            result.Body = reader.ReadToEnd();
                            break;
                        }

                        var colon = line.IndexOf(':');
                        result.Headers[line.Substring(0, colon)] = line.Substring(colon + 1).Trim();
                    }
                }
            }

            return result;
        }

        public void Save(FileSystemInfo root)
        {
            if (null == root)
            {
                throw new ArgumentNullException("root");
            }

            if (string.IsNullOrEmpty(Headers["urn"]))
            {
                throw new InvalidOperationException("The record URN must be specified.");
            }

            if (string.IsNullOrEmpty(Headers["key"]))
            {
                throw new InvalidOperationException("The record key must be specified.");
            }

            AbsoluteUri urn = Headers["urn"];
            var file = new FileInfo(Path.Combine(
                urn.ToPath(root).FullName,
                "{0}.record".FormatWith(Headers["key"])));

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            using (var stream = file.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine("urn: {0}".FormatWith(Headers["urn"]));
                    writer.WriteLine("key: {0}".FormatWith(Headers["key"]));
                    writer.WriteLine("etag: {0}".FormatWith(Headers["etag"]));
                    writer.WriteLine("created: {0}".FormatWith(Headers["created"]));
                    writer.WriteLine("modified: {0}".FormatWith(Headers["modified"]));
                    writer.WriteLine("cacheability: {0}".FormatWith(Headers["cacheability"]));
                    writer.WriteLine("expiration: {0}".FormatWith(Headers["expiration"]));
                    writer.WriteLine("status: {0}".FormatWith(Headers["status"]));
                    writer.WriteLine(string.Empty);
                    if (null != Body)
                    {
                        writer.Write(Body);
                    }
                }
            }

            Location = file;
        }

        public IRecord<T> ToRecord<T>()
        {
            return new Record<T>
            {
                Cacheability = Headers["cacheability"],
                Created = XmlConvert.ToDateTime(Headers["created"], XmlDateTimeSerializationMode.Utc),
                Etag = Headers["etag"],
                Expiration = Headers["expiration"],
                Key = AlphaDecimal.FromString(Headers["key"]),
                Modified = XmlConvert.ToDateTime(Headers["modified"], XmlDateTimeSerializationMode.Utc),
                Status = XmlConvert.ToInt32(Headers["status"]),
                Urn = Headers["urn"],
                Value = Body.XmlDeserialize<T>()
            };
        }

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