namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Threading;
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

        public static bool EndsWithAny(this string obj, StringComparison comparison, params string[] args)
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.EndsWith(arg, comparison));
        }

        public static bool EqualsAny(this string obj, StringComparison comparison, params string[] args)
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.Equals(arg, comparison));
        }

        public static string FormatWith(this string obj, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, obj, args);
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "Space is not wasted.")]
        public static int LevenshteinDistance(this string obj, string comparand)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return null == comparand ? 0 : comparand.Length;
            }

            if (string.IsNullOrEmpty(comparand))
            {
                return obj.Length;
            }

            var n = obj.Length;
            var m = comparand.Length;
            var d = new int[n + 1, m + 1];

            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = (comparand[j - 1] == obj[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        public static string NormalizeWhiteSpace(this string obj)
        {
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

            foreach (var c in obj)
            {
                buffer.Append(c.IsWhiteSpace() ? ' ' : c);
            }

            return buffer.ToString();
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
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

            foreach (var c in obj)
            {
                if (c.IsWhiteSpace())
                {
                    continue;
                }

                buffer.Append(c);
            }

            return buffer.ToString();
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

        public static bool SameIndexesOfEach(this string obj, params char[] args)
        {
            return string.IsNullOrEmpty(obj) || args.All(arg => obj.IndexOf(arg) == obj.LastIndexOf(arg));
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

        public static bool StartsOrEndsWith(this string obj, params char[] args)
        {
            return !string.IsNullOrEmpty(obj) && args.Any(arg => arg.Equals(obj[0]) || arg.Equals(obj[obj.Length - 1]));
        }

        public static bool StartsWithAny(this string obj, StringComparison comparison, params string[] args)
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.StartsWith(arg, comparison));
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Title casing only works from lower case strings.")]
        public static string ToTitleCase(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var info = Thread.CurrentThread.CurrentUICulture.TextInfo;

            return info.ToTitleCase(obj.ToLowerInvariant());
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