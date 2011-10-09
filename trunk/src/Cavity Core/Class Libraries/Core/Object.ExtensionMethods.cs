namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.XPath;
    using Cavity.IO;
    using Cavity.Properties;
    using Cavity.Xml;

    public static class ObjectExtensionMethods
    {
#if NET20
        public static bool EqualsOneOf<T>(T obj,
                                          params T[] args)
#else
        public static bool EqualsOneOf<T>(this T obj,
                                          params T[] args)
#endif
        {
#if NET20
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.Equals(obj))
                {
                    return true;
                }
            }

            return false;
#else
            return args.Contains(obj);
#endif
        }

#if NET20
        public static bool IsBoundedBy<T>(T obj,
                                          T lower,
                                          T upper)
#else
        public static bool IsBoundedBy<T>(this T obj,
                                          T lower,
                                          T upper)
#endif
            where T : IComparable<T>
        {
            if (ReferenceEquals(null, upper))
            {
                throw new ArgumentNullException("upper");
            }

            if (1 > upper.CompareTo(lower))
            {
                throw new ArgumentException(Resources.ObjectExtensionMethods_IsBoundedBy_Message);
            }

            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return -1 < obj.CompareTo(lower) && 1 > obj.CompareTo(upper);
        }

#if NET20
        public static string ToXmlString(object value)
#else
        public static string ToXmlString(this object value)
#endif
        {
            if (null == value)
            {
                return null;
            }

            var s = value as string;
            if (null != s)
            {
                return s;
            }

            if (value is bool)
            {
                return XmlConvert.ToString((bool)value);
            }

            if (value is DateTime)
            {
                return XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Utc);
            }

#if !NET20
            if (value is DateTimeOffset)
            {
                return XmlConvert.ToString((DateTimeOffset)value);
            }
#endif

            if (value is decimal)
            {
                return XmlConvert.ToString((decimal)value);
            }

            if (value is double)
            {
                return XmlConvert.ToString((double)value);
            }

            if (value is float)
            {
                return XmlConvert.ToString((float)value);
            }

            if (value is TimeSpan)
            {
                return XmlConvert.ToString((TimeSpan)value);
            }

            if (value is IConvertible)
            {
                return (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
            }

            return value.ToString();
        }

#if NET20
        public static IXPathNavigable XmlSerialize(object value)
#else
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static IXPathNavigable XmlSerialize(this object value)
#endif
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var result = new XmlDocument();

            var buffer = new StringBuilder();

            var exception = value as Exception;
            if (null != exception)
            {
                using (var stream = new MemoryStream())
                {
                    new SoapFormatter().Serialize(stream, value);
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                    {
                        buffer.Append(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                using (TextWriter writer = new EncodedStringWriter(buffer, CultureInfo.InvariantCulture, Encoding.UTF8))
                {
                    XmlSerializerNamespaces namespaces;
                    var obj = value as IXmlSerializerNamespaces;
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