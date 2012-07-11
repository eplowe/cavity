namespace Cavity
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Cavity.Data;

    public static class JsonObjectExtensionMethods
    {
        public static string JsonSerialize(this object value)
        {
            if (null == value)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    var list = value as IEnumerable;
                    if (null == list)
                    {
                        writer.Object();
                        writer.JsonSerializeObject(value);
                    }
                    else
                    {
                        writer.Array();
                        writer.JsonSerializeList(list);
                    }
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static bool JsonSerializeBuiltInType(this JsonWriter writer, 
                                                     string name, 
                                                     object value)
        {
            if (writer.JsonSerializeIntegralType(name, value))
            {
                return true;
            }

            if (writer.JsonSerializeFloatingPointType(name, value))
            {
                return true;
            }

            if (value is bool)
            {
                writer.Pair(name, (bool)value);
                return true;
            }

            if (value is decimal)
            {
                writer.Pair(name, (decimal)value);
                return true;
            }

            var str = value as string;
            if (null != str)
            {
                writer.Pair(name, str);
                return true;
            }

            return false;
        }

        private static bool JsonSerializeFloatingPointType(this JsonWriter writer, 
                                                           string name, 
                                                           object value)
        {
            if (value is double)
            {
                writer.Pair(name, (double)value);
                return true;
            }

            if (value is float)
            {
                writer.Pair(name, (float)value);
                return true;
            }

            return false;
        }

        private static bool JsonSerializeIntegralType(this JsonWriter writer, 
                                                      string name, 
                                                      object value)
        {
            if (value is byte)
            {
                writer.Pair(name, (byte)value);
                return true;
            }

            if (value is char)
            {
                writer.Pair(name, (char)value);
                return true;
            }

            if (value is short)
            {
                writer.Pair(name, (short)value);
                return true;
            }

            if (value is int)
            {
                writer.Pair(name, (int)value);
                return true;
            }

            if (value is long)
            {
                writer.Pair(name, (long)value);
                return true;
            }

            return false;
        }

        private static void JsonSerializeList(this JsonWriter writer, 
                                              IEnumerable list)
        {
            foreach (var item in list)
            {
                writer.Object();
                writer.JsonSerializeObject(item);
            }

            writer.EndArray();
        }

        private static void JsonSerializeObject(this JsonWriter writer, 
                                                object obj)
        {
            if (null == obj)
            {
                return;
            }

            var properties = obj
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList();
            foreach (var property in properties)
            {
                var name = property.Name.ToCamelCase();
                var value = property.GetValue(obj, null);
                if (null == value)
                {
                    writer.NullPair(name);
                    continue;
                }

                if (writer.JsonSerializeBuiltInType(name, value))
                {
                    continue;
                }

                if (value is DateTime)
                {
                    writer.Pair(name, (DateTime)value);
                    continue;
                }

                if (value is DateTimeOffset)
                {
                    writer.Pair(name, (DateTimeOffset)value);
                    continue;
                }

                if (value is Guid)
                {
                    writer.Pair(name, (Guid)value);
                    continue;
                }

                if (value is TimeSpan)
                {
                    writer.Pair(name, (TimeSpan)value);
                    continue;
                }

                if (value is Enum)
                {
                    writer.Pair(name, value.ToString());
                    continue;
                }

                if (value is ValueType)
                {
                    if (0 == properties.Count)
                    {
                        writer.Pair(name, value.ToString());
                        continue;
                    }

                    writer.Object(name);
                    writer.JsonSerializeObject(value);
                }
            }

            writer.EndObject();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Camel casing starts with a lower case letter.")]
        private static string ToCamelCase(this string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (0 == value.Length)
            {
                throw new ArgumentNullException("value");
            }

            return value[0].ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + value.Substring(1);
        }
    }
}