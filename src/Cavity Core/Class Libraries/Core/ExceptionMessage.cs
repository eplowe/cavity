namespace Cavity
{
    using System.Globalization;
    using Cavity.Properties;

    public static class ExceptionMessage
    {
        public static string IndexAfterValueLength(int index,
                                                   string value)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_IndexAfterValueLength,
                                 index,
                                 value);
        }

        public static string LengthShorterThanValueLength(int length,
                                                          string value)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_LengthShorterThanValueLength,
                                 length,
                                 value);
        }

        public static string NegativeIndex(int index)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_NegativeIndex,
                                 index);
        }

        public static string NegativeLength(int start)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_NegativeLength,
                                 start);
        }

        public static string NegativeStartIndex(int start)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_NegativeStartIndex,
                                 start);
        }

        public static string StartIndexAfterValueLength(int start,
                                                        string value)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_StartIndexAfterValueLength,
                                 start,
                                 value);
        }

        public static string StartIndexAndLengthAfterValueLength(int start,
                                                                 int length,
                                                                 string value)
        {
            return string.Format(
                                 CultureInfo.InvariantCulture,
                                 Resources.ExceptionMessage_StartIndexAndLengthAfterValueLength,
                                 start,
                                 length,
                                 value);
        }
    }
}