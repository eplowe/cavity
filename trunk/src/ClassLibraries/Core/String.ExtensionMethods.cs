namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Xml.Serialization;

    public static class StringExtensionMethods
    {
        public static bool Contains(this string obj, string value, StringComparison comparisonType)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return -1 != obj.IndexOf(value, comparisonType);
        }

        public static string FormatWith(this string obj, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, obj, args);
        }

        public static string RemoveAny(this string obj, params char[] args)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

            return 0 == obj.Length
                       ? string.Empty
                       : args.Aggregate(obj, (current, arg) => current.Replace(arg.ToString(), string.Empty));
        }

        public static string RemoveAnyDigits(this string obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            var buffer = new StringBuilder(obj.Length);

            foreach (var c in obj)
            {
                if (char.IsDigit(c))
                {
                    continue;
                }

                buffer.Append(c);
            }

            return buffer.ToString();
        }

        public static string RemoveAnyWhiteSpace(this string obj)
        {
            return obj.RemoveAny(
                '\u0009',
                //// HT (Horizontal Tab)
                '\u000A',
                //// LF (Line Feed)
                '\u000B',
                //// VT (Vertical Tab)
                '\u000C',
                //// FF (Form Feed)
                '\u000D',
                //// CR (Carriage Return)
                '\u0020',
                //// Space
                '\u0085',
                //// NEL (control character next line)
                '\u00A0',
                //// No-Break Space
                '\u1680',
                //// Ogham Space Mark
                '\u180E',
                //// Mongolian Vowel Separator
                '\u2000',
                //// En quad
                '\u2001',
                //// Em quad
                '\u2002',
                //// En Space
                '\u2003',
                //// Em Space
                '\u2004',
                //// Three-Per-Em Space
                '\u2005',
                //// Four-Per-Em Space
                '\u2006',
                //// Six-Per-Em Space
                '\u2007',
                //// Figure Space
                '\u2008',
                //// Punctuation Space
                '\u2009',
                //// Thin Space
                '\u200A',
                //// Hair Space
                '\u200B',
                //// Zero Width Space
                '\u200C',
                //// Zero Width Non Joiner
                '\u200D',
                //// Zero Width Joiner
                '\u2028',
                //// Line Separator
                '\u2029',
                //// Paragraph Separator
                '\u202F',
                //// Narrow No-Break Space
                '\u205F',
                //// Medium Mathematical Space
                '\u2060',
                //// Word Joiner
                '\u3000',
                //// Ideographic Space
                '\uFEFF'); //// Zero Width No-Break Space
        }

        public static string Replace(this string obj, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == oldValue)
            {
                throw new ArgumentNullException("oldValue");
            }

            if (0 == obj.Length)
            {
                return obj;
            }

            if (0 == oldValue.Length)
            {
                return obj;
            }

            var buffer = new StringBuilder();
            for (var i = 0; i < obj.Length; i++)
            {
                if (obj.Substring(i).StartsWith(oldValue, comparisonType))
                {
                    buffer.Append(newValue);
                    i += oldValue.Length - 1;
                    continue;
                }

                buffer.Append(obj[i]);
            }

            return buffer.ToString();
        }

        public static string[] Split(this string obj, char separator, StringSplitOptions options)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.Split(
                new[]
                {
                    separator
                },
                options);
        }

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

            if (0 == xml.Length)
            {
                throw new ArgumentOutOfRangeException("xml");
            }

            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();
                    stream.Position = 0;
                    return typeof(Exception).IsAssignableFrom(type)
                               ? new SoapFormatter().Deserialize(stream)
                               : new XmlSerializer(type).Deserialize(stream);
                }
            }
        }
    }
}