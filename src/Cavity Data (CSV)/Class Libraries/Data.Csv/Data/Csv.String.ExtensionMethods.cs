namespace Cavity.Data
{
    using System;

    public static class CsvStringExtensionMethods
    {
#if NET20
        public static string FormatCommaSeparatedValue(string value)
#else
        public static string FormatCommaSeparatedValue(this string value)
#endif
        {
            if (null == value)
            {
                return null;
            }

#if NET20
            value = StringExtensionMethods.Replace(value, "\"", "\"\"", StringComparison.Ordinal);
#else
            value = value.Replace("\"", "\"\"", StringComparison.Ordinal);
#endif

            return value.Contains(",")
                       ? string.Concat("\"", value, "\"")
                       : value;
        }
    }
}