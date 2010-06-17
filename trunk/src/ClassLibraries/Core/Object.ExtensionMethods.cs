namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.XPath;
    using Cavity.IO;
    using Cavity.Xml;

    public static class ObjectExtensionMethods
    {
        public static string ToXmlString(this object value)
        {
            string result = null;

            if (null != value)
            {
                string s = value as string;
                if (null != s)
                {
                    result = s;
                }
                else if (value is bool)
                {
                    result = XmlConvert.ToString((bool)value);
                }
                else if (value is DateTime)
                {
                    result = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Utc);
                }
                else if (value is DateTimeOffset)
                {
                    result = XmlConvert.ToString((DateTimeOffset)value);
                }
                else if (value is decimal)
                {
                    result = XmlConvert.ToString((decimal)value);
                }
                else if (value is double)
                {
                    result = XmlConvert.ToString((double)value);
                }
                else if (value is float)
                {
                    result = XmlConvert.ToString((float)value);
                }
                else if (value is TimeSpan)
                {
                    result = XmlConvert.ToString((TimeSpan)value);
                }
                else if (value is IConvertible)
                {
                    result = (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
                }
                else
                {
                    result = value.ToString();
                }
            }

            return result;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static IXPathNavigable XmlSerialize(this object value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            XmlDocument result = new XmlDocument();

            StringBuilder buffer = new StringBuilder();

            Exception exception = value as Exception;
            if (null != exception)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    new SoapFormatter().Serialize(stream, value);
                    stream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        buffer.Append(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                using (TextWriter writer = new EncodedStringWriter(buffer, CultureInfo.InvariantCulture, Encoding.UTF8))
                {
                    XmlSerializerNamespaces namespaces = null;
                    IXmlSerializerNamespaces obj = value as IXmlSerializerNamespaces;
                    if (null == obj)
                    {
                        namespaces = new XmlSerializerNamespaces();
                        namespaces.Add(string.Empty, string.Empty);
                    }
                    else
                    {
                        namespaces = obj.XmlNamespaceDeclarations;
                    }

                    new XmlSerializer(value.GetType()).Serialize(writer, value, namespaces);
                }
            }

            result.LoadXml(buffer.ToString());

            return result;
        }
    }
}