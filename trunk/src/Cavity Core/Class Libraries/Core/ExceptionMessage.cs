namespace Cavity
{
    using Cavity.Properties;

    public static class ExceptionMessage
    {
        public static string IndexAfterValueLength(int index, 
                                                   string value)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_IndexAfterValueLength, index, value);
#else
            return Resources.ExceptionMessage_IndexAfterValueLength.FormatWith(index, value);
#endif
        }

        public static string LengthShorterThanValueLength(int length, 
                                                          string value)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_LengthShorterThanValueLength, length, value);
#else
            return Resources.ExceptionMessage_LengthShorterThanValueLength.FormatWith(length, value);
#endif
        }

        public static string NegativeIndex(int index)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_NegativeIndex, index);
#else
            return Resources.ExceptionMessage_NegativeIndex.FormatWith(index);
#endif
        }

        public static string NegativeLength(int start)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_NegativeLength, start);
#else
            return Resources.ExceptionMessage_NegativeLength.FormatWith(start);
#endif
        }

        public static string NegativeStartIndex(int start)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_NegativeStartIndex, start);
#else
            return Resources.ExceptionMessage_NegativeStartIndex.FormatWith(start);
#endif
        }

        public static string StartIndexAfterValueLength(int start, 
                                                        string value)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_StartIndexAfterValueLength, start, value);
#else
            return Resources.ExceptionMessage_StartIndexAfterValueLength.FormatWith(start, value);
#endif
        }

        public static string StartIndexAndLengthAfterValueLength(int start, 
                                                                 int length, 
                                                                 string value)
        {
#if NET20
            return StringExtensionMethods.FormatWith(Resources.ExceptionMessage_StartIndexAndLengthAfterValueLength, start, length, value);
#else
            return Resources.ExceptionMessage_StartIndexAndLengthAfterValueLength.FormatWith(start, length, value);
#endif
        }
    }
}