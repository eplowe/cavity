namespace Cavity.Data
{
    public static class DataStringExtensionMethods
    {
        public static string FormatCommaSeparatedValue(this string value)
        {
            if (null == value)
            {
                return null;
            }

            return value.Contains(",")
                ? string.Concat("\"", value, "\"")
                : value;
        }
    }
}