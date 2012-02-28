namespace Cavity
{
    using System;
    ////using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.XPath;

    public static class StringExtensionMethods
    {
#if NET20
        public static string Append(string obj, 
                                    params char[] args)
#else
        public static string Append(this string obj,
                                    params char[] args)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                return obj;
            }

            switch (args.Length)
            {
                case 0:
                    return obj;
                case 1:
                    return string.Concat(obj, args[0]);
                case 2:
                    return string.Concat(obj, args[0], args[1]);
                case 3:
                    return string.Concat(obj, args[0], args[1], args[2]);
                case 4:
                    return string.Concat(obj, args[0], args[1], args[2], args[3]);
                case 5:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    var buffer = new StringBuilder(obj);
                    foreach (var arg in args)
                    {
                        buffer.Append(arg);
                    }

                    return buffer.ToString();
            }
        }

#if NET20
        public static string Append(string obj, 
                                    params string[] args)
#else
        public static string Append(this string obj,
                                    params string[] args)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                return obj;
            }

            switch (args.Length)
            {
                case 0:
                    return obj;
                case 1:
                    return string.Concat(obj, args[0]);
                case 2:
                    return string.Concat(obj, args[0], args[1]);
                case 3:
                    return string.Concat(obj, args[0], args[1], args[2]);
                case 4:
                    return string.Concat(obj, args[0], args[1], args[2], args[3]);
                case 5:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    var buffer = new StringBuilder(obj);
                    foreach (var arg in args)
                    {
                        buffer.Append(arg);
                    }

                    return buffer.ToString();
            }
        }

#if NET20
        public static bool Contains(string obj, 
                                    string value, 
                                    StringComparison comparisonType)
#else
        public static bool Contains(this string obj, 
                                    string value, 
                                    StringComparison comparisonType)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return -1 != obj.IndexOf(value, comparisonType);
        }

#if NET20
        public static bool ContainsAny(string obj, 
                                       params char[] args)
#else
        public static bool ContainsAny(this string obj,
                                       params char[] args)
#endif
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

#if NET20
            foreach (var arg in args)
            {
                if (-1 == obj.IndexOf(arg))
                {
                    continue;
                }

                return true;
            }

            return false;
#else
            return args.Any(arg => -1 != obj.IndexOf(arg));
#endif
        }

#if NET20
        public static bool ContainsAny(string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#else
        public static bool ContainsAny(this string obj,
                                       StringComparison comparison, 
                                       params string[] args)
#endif
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

#if NET20
            foreach (var arg in args)
            {
                if (-1 == obj.IndexOf(arg, comparison))
                {
                    continue;
                }

                return true;
            }

            return false;
#else
            return args.Any(arg => -1 != obj.IndexOf(arg, comparison));
#endif
        }

#if !NET20
        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

#endif

#if NET20
        public static bool IsNullOrWhiteSpace(string obj)
#else
        public static bool IsNullOrWhiteSpace(this string obj)
#endif
        {
#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return true;
            }

            foreach (var c in obj)
            {
                if (!' '.Equals(c))
                {
                    return false;
                }
            }

            return true;
#else
            return string.IsNullOrEmpty(obj) || obj.All(c => ' '.Equals(c));

#endif
        }

#if NET20
        public static bool EndsWithAny(string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#else
        public static bool EndsWithAny(this string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#endif
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

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.EndsWith(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.EndsWith(arg, comparison));
#endif
        }

#if NET20
        public static bool EqualsAny(string obj, 
                                     StringComparison comparison, 
                                     params string[] args)
#else
        public static bool EqualsAny(this string obj, 
                                     StringComparison comparison, 
                                     params string[] args)
#endif
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

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.Equals(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.Equals(arg, comparison));
#endif
        }

#if NET20
        public static string FormatWith(string obj, 
                                        params object[] args)
#else
        public static string FormatWith(this string obj, 
                                        params object[] args)
#endif
        {
            return string.Format(CultureInfo.InvariantCulture, obj, args);
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "Space is not wasted.")]
#if NET20
        public static int LevenshteinDistance(string obj, 
                                              string comparand)
#else
        public static int LevenshteinDistance(this string obj, 
                                              string comparand)
#endif
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

#if NET20
        public static string NormalizeWhiteSpace(string obj)
#else
        public static string NormalizeWhiteSpace(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

            foreach (var c in obj)
            {
#if NET20
                buffer.Append(CharExtensionMethods.IsWhiteSpace(c) ? ' ' : c);
#else
                buffer.Append(c.IsWhiteSpace() ? ' ' : c);
#endif
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAny(string obj, 
                                       params char[] args)
#else
        public static string RemoveAny(this string obj, 
                                       params char[] args)
#endif
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

#if NET20
            if (0 == obj.Length)
            {
                return string.Empty;
            }

            foreach (var arg in args)
            {
                obj = obj.Replace(arg.ToString(), string.Empty);
            }

            return obj;
#else
            return 0 == obj.Length
                       ? string.Empty
                       : args.Aggregate(obj, 
                                        (current, 
                                         arg) => current.Replace(arg.ToString(CultureInfo.InvariantCulture), string.Empty));
#endif
        }

#if NET20
        public static string RemoveAnyDigits(string obj)
#else
        public static string RemoveAnyDigits(this string obj)
#endif
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

#if NET20
            foreach (var c in obj)
            {
                if (char.IsDigit(c))
                {
                    continue;
                }

                buffer.Append(c);
            }
#else
            foreach (var c in obj.Where(c => !char.IsDigit(c)))
            {
                buffer.Append(c);
            }

#endif

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyWhiteSpace(string obj)
#else
        public static string RemoveAnyWhiteSpace(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

#if NET20
            foreach (var c in obj)
            {
                if (CharExtensionMethods.IsWhiteSpace(c))
                {
                    continue;
                }

                buffer.Append(c);
            }
#else
            foreach (var c in obj.Where(c => !c.IsWhiteSpace()))
            {
                buffer.Append(c);
            }

#endif

            return buffer.ToString();
        }

#if NET20
        public static string RemoveDefiniteArticle(string obj)
#else
        public static string RemoveDefiniteArticle(this string obj)
#endif
        {
            return RemoveFromStart(obj, "THE", StringComparison.OrdinalIgnoreCase);
        }

#if NET20
        public static string RemoveFromEnd(string obj, 
                                           string value, 
                                           StringComparison comparisonType)
#else
        public static string RemoveFromEnd(this string obj, 
                                           string value, 
                                           StringComparison comparisonType)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return obj.EndsWith(value, comparisonType)
                       ? obj.Substring(0, obj.Length - value.Length)
                       : obj;
        }

#if NET20
        public static string RemoveFromStart(string obj, 
                                             string value, 
                                             StringComparison comparisonType)
#else
        public static string RemoveFromStart(this string obj, 
                                             string value, 
                                             StringComparison comparisonType)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return obj.StartsWith(value, comparisonType)
                       ? obj.Substring(value.Length)
                       : obj;
        }

#if NET20
        public static string Replace(string obj, 
                                     string oldValue, 
                                     string newValue, 
                                     StringComparison comparisonType)
#else
        public static string Replace(this string obj, 
                                     string oldValue, 
                                     string newValue, 
                                     StringComparison comparisonType)
#endif
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

#if NET20
        public static string ReplaceAllWith(string obj, 
                                            string newValue, 
                                            StringComparison comparisonType, 
                                            params string[] args)
#else
        public static string ReplaceAllWith(this string obj, 
                                            string newValue, 
                                            StringComparison comparisonType, 
                                            params string[] args)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == newValue)
            {
                throw new ArgumentNullException("newValue");
            }

            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

#if NET20
            foreach (var arg in args)
            {
                obj = Replace(obj, arg, newValue, comparisonType);
            }

            return obj;
#else
            return args.Aggregate(obj, 
                                  (x, 
                                   arg) => x.Replace(arg, newValue, comparisonType));
#endif
        }

#if NET20
        public static bool SameLengthAs(string obj,
                                        string value)
#else
        public static bool SameLengthAs(this string obj,
                                        string value)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return obj.Length == value.Length;
        }

#if NET20
        public static bool SameIndexesOfEach(string obj, 
                                             params char[] args)
#else
        public static bool SameIndexesOfEach(this string obj, 
                                             params char[] args)
#endif
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return true;
            }

            foreach (var arg in args)
            {
                if (obj.IndexOf(arg) != obj.LastIndexOf(arg))
                {
                    return false;
                }
            }

            return true;
#else
            return string.IsNullOrEmpty(obj) || args.All(arg => obj.IndexOf(arg) == obj.LastIndexOf(arg));
#endif
        }

#if NET20
        public static string[] Split(string obj, 
                                     char separator, 
                                     StringSplitOptions options)
#else
        public static string[] Split(this string obj, 
                                     char separator, 
                                     StringSplitOptions options)
#endif
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

#if NET20
        public static string[] Split(string obj, 
                                     string separator, 
                                     StringSplitOptions options)
#else
        public static string[] Split(this string obj,
                                     string separator,
                                     StringSplitOptions options)
#endif
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

#if NET20
        public static bool StartsOrEndsWith(string obj, 
                                            params char[] args)
#else
        public static bool StartsOrEndsWith(this string obj, 
                                            params char[] args)
#endif
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.Equals(obj[0]))
                {
                    return true;
                }

                if (arg.Equals(obj[obj.Length - 1]))
                {
                    return true;
                }
            }

            return false;
#else
            return !string.IsNullOrEmpty(obj) && args.Any(arg => arg.Equals(obj[0]) || arg.Equals(obj[obj.Length - 1]));
#endif
        }

#if NET20
        public static bool StartsWithAny(string obj, 
                                         StringComparison comparison, 
                                         params string[] args)
#else
        public static bool StartsWithAny(this string obj, 
                                         StringComparison comparison, 
                                         params string[] args)
#endif
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

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.StartsWith(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.StartsWith(arg, comparison));
#endif
        }

#if NET20
        public static T To<T>(string obj)
#else
        public static T To<T>(this string obj)
#endif
        {
            var type = typeof(T);
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return string.IsNullOrEmpty(obj)
                           ? default(T)
                           : To<T>(Nullable.GetUnderlyingType(type), obj);
            }

            return To<T>(type, obj);
        }

#if NET20
        public static T TryTo<T>(string obj)
#else
        public static T TryTo<T>(this string obj)
#endif
        {
            var type = typeof(T);
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return string.IsNullOrEmpty(obj)
                           ? default(T)
                           : TryTo<T>(Nullable.GetUnderlyingType(type), obj);
            }

            return TryTo<T>(type, obj);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Title casing only works from lower case strings.")]
#if NET20
        public static string ToTitleCase(string obj)
#else
        public static string ToTitleCase(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var info = Thread.CurrentThread.CurrentUICulture.TextInfo;

            return info.ToTitleCase(obj.ToLowerInvariant());
        }

#if NET20
        public static IXPathNavigable XmlDeserialize(string xml)
#else
        public static IXPathNavigable XmlDeserialize(this string xml)
#endif
        {
            var result = new XmlDocument();
            result.LoadXml(xml);

            return result;
        }

#if NET20
        public static T XmlDeserialize<T>(string xml)
#else
        public static T XmlDeserialize<T>(this string xml)
#endif
        {
            return (T)XmlDeserialize(xml, typeof(T));
        }

#if NET20
        public static object XmlDeserialize(string xml, 
                                            Type type)
#else
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static object XmlDeserialize(this string xml, 
                                            Type type)
#endif
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

#if NET20
        private static T To<T>(Type type, string obj)
#else
        private static T To<T>(this Type type, 
                               string obj)
#endif
        {
            object value;
            if (typeof(bool) == type)
            {
                value = XmlConvert.ToBoolean(obj);
            }
            else if (typeof(byte) == type)
            {
                value = XmlConvert.ToByte(obj);
            }
            else if (typeof(char) == type)
            {
                value = XmlConvert.ToChar(obj);
            }
            else if (typeof(DateTime) == type)
            {
                value = XmlConvert.ToDateTime(obj, XmlDateTimeSerializationMode.Utc);
            }

#if !NET20
            else if (typeof(DateTimeOffset) == type)
            {
                value = XmlConvert.ToDateTimeOffset(obj);
            }

#endif
            else if (typeof(decimal) == type)
            {
                value = XmlConvert.ToDecimal(obj);
            }
            else if (typeof(double) == type)
            {
                value = XmlConvert.ToDouble(obj);
            }
            else if (typeof(Guid) == type)
            {
                value = XmlConvert.ToGuid(obj);
            }
            else if (typeof(short) == type)
            {
                value = XmlConvert.ToInt16(obj);
            }
            else if (typeof(int) == type)
            {
                value = XmlConvert.ToInt32(obj);
            }
            else if (typeof(long) == type)
            {
                value = XmlConvert.ToInt64(obj);
            }
            else if (typeof(sbyte) == type)
            {
                value = XmlConvert.ToSByte(obj);
            }
            else if (typeof(float) == type)
            {
                value = XmlConvert.ToSingle(obj);
            }
            else if (typeof(TimeSpan) == type)
            {
                value = XmlConvert.ToTimeSpan(obj);
            }
            else if (typeof(ushort) == type)
            {
                value = XmlConvert.ToUInt16(obj);
            }
            else if (typeof(uint) == type)
            {
                value = XmlConvert.ToUInt32(obj);
            }
            else if (typeof(ulong) == type)
            {
                value = XmlConvert.ToUInt32(obj);
            }
            else
            {
                value = obj;
            }

            return (T)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is due to the type-specific nature of parsing.")]
#if NET20
        private static T TryTo<T>(Type type, string obj)
#else
        private static T TryTo<T>(this Type type, 
                                  string obj)
#endif
        {
            if (typeof(bool) == type)
            {
                bool boolResult;
                return bool.TryParse(obj, out boolResult)
                           ? (T)Convert.ChangeType(boolResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(byte) == type)
            {
                byte byteResult;
                return byte.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out byteResult)
                           ? (T)Convert.ChangeType(byteResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(char) == type)
            {
                char charResult;
                return char.TryParse(obj, out charResult)
                           ? (T)Convert.ChangeType(charResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(DateTime) == type)
            {
                DateTime dateTimeResult;
                return DateTime.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateTimeResult)
                           ? (T)Convert.ChangeType(dateTimeResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#if !NET20
            if (typeof(DateTimeOffset) == type)
            {
                DateTimeOffset dateTimeOffsetResult;
                return DateTimeOffset.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeOffsetResult)
                           ? (T)Convert.ChangeType(dateTimeOffsetResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif

            if (typeof(decimal) == type)
            {
                decimal decimalResult;
                return decimal.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalResult)
                           ? (T)Convert.ChangeType(decimalResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(double) == type)
            {
                double doubleResult;
                return double.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out doubleResult)
                           ? (T)Convert.ChangeType(doubleResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#if NET40
            if (typeof(Guid) == type)
            {
                Guid guidResult;
                return Guid.TryParse(obj, out guidResult)
                           ? (T)Convert.ChangeType(guidResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif
            if (typeof(short) == type)
            {
                short shortResult;
                return short.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out shortResult)
                           ? (T)Convert.ChangeType(shortResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(int) == type)
            {
                int intResult;
                return int.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out intResult)
                           ? (T)Convert.ChangeType(intResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(long) == type)
            {
                long longResult;
                return long.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out longResult)
                           ? (T)Convert.ChangeType(longResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(sbyte) == type)
            {
                sbyte sbyteResult;
                return sbyte.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out sbyteResult)
                           ? (T)Convert.ChangeType(sbyteResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(float) == type)
            {
                float floatResult;
                return float.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out floatResult)
                           ? (T)Convert.ChangeType(floatResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(string) == type)
            {
                return (T)Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
            }

#if NET40
            if (typeof(TimeSpan) == type)
            {
                TimeSpan timeSpanResult;
                return TimeSpan.TryParse(obj, CultureInfo.InvariantCulture, out timeSpanResult)
                           ? (T)Convert.ChangeType(timeSpanResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif
            if (typeof(ushort) == type)
            {
                ushort ushortResult;
                return ushort.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out ushortResult)
                           ? (T)Convert.ChangeType(ushortResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(uint) == type)
            {
                uint uintResult;
                return uint.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out uintResult)
                           ? (T)Convert.ChangeType(uintResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(ulong) == type)
            {
                ulong ulongResult;
                return ulong.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out ulongResult)
                           ? (T)Convert.ChangeType(ulongResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            return default(T);
        }
    }
}