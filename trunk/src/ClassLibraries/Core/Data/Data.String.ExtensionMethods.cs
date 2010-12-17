namespace Cavity.Data
{
    using System;

    public static class DataStringExtensionMethods
    {
        public static string FormatCommaSeparatedValue(this string value)
        {
            if (null == value)
            {
                return null;
            }

            value = value.Replace("\"", "\"\"", StringComparison.Ordinal);

            return value.Contains(",")
                       ? string.Concat("\"", value, "\"")
                       : value;
        }
    }
}