namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Xml.Serialization;

    public static class StringExtensionMethods
    {
        public static T XmlDeserialize<T>(this string xml)
        {
            return (T)XmlDeserialize(xml, typeof(T));
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static object XmlDeserialize(this string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            else if (0 == xml.Length)
            {
                throw new ArgumentOutOfRangeException("xml");
            }
            else if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            object result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();
                    stream.Position = 0;
                    result = typeof(Exception).IsAssignableFrom(type)
                        ? new SoapFormatter().Deserialize(stream)
                        : new XmlSerializer(type).Deserialize(stream);
                }
            }

            return result;
        }
    }
}