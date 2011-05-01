namespace Cavity
{
    using System;
    using System.Globalization;

    public static class DateTimeExtensionMethods
    {
        public static string ToFileName(this DateTime obj)
        {
            return obj.ToUniversalTime().ToString(@"yyyy-MM-dd HH\hmm ss,fff G\MT", CultureInfo.InvariantCulture);
        }

        public static DateTime ToLocalTime(this DateTime obj,
                                           string zone)
        {
            if (null == zone)
            {
                throw new ArgumentNullException("zone");
            }

            if (0 == zone.Length)
            {
                throw new ArgumentOutOfRangeException("zone");
            }

            return obj.ToLocalTime(TimeZoneInfo.FindSystemTimeZoneById(zone));
        }

        public static DateTime ToLocalTime(this DateTime obj,
                                           TimeZoneInfo zone)
        {
            if (null == zone)
            {
                throw new ArgumentNullException("zone");
            }

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(obj, zone.Id);
        }
    }
}