namespace Cavity
{
    using System;
    using System.Globalization;

    public static class TimeSpanExtensionMethods
    {
#if NET20
        public static string PrettyPrint(TimeSpan value)
#else
        public static string PrettyPrint(this TimeSpan value)
#endif
        {
            return string.Format(CultureInfo.InvariantCulture,
                                 "{0} {1} {2}",
                                 PrettyPrint((value.Days * 24) + value.Hours, 'h'),
                                 PrettyPrint(value.Minutes, 'm'),
                                 PrettyPrint(value.Seconds, 's'));
        }

        private static string PrettyPrint(int value,
                                          char c)
        {
            return 0 == value
                       ? "   "
                       : string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", value < 10 ? " " : string.Empty, value, c);
        }
    }
}