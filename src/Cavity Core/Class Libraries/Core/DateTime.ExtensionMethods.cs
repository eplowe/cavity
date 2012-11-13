namespace Cavity
{
    using System;
    using System.Globalization;

    public static class DateTimeExtensionMethods
    {
#if NET20
        public static Date ToDate(DateTime value)
#else
        public static Date ToDate(this DateTime value)
#endif
        {
            return new Date(value);
        }

#if NET20
        public static string ToFileName(DateTime value)
#else
        public static string ToFileName(this DateTime value)
#endif
        {
            return value.ToUniversalTime().ToString(@"yyyy-MM-dd HH\hmm ss,fff G\MT", CultureInfo.InvariantCulture);
        }

#if !NET20
        public static DateTime ToLocalTime(this DateTime value, 
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

            return value.ToLocalTime(TimeZoneInfo.FindSystemTimeZoneById(zone));
        }

        public static DateTime ToLocalTime(this DateTime value, 
                                           TimeZoneInfo zone)
        {
            if (null == zone)
            {
                throw new ArgumentNullException("zone");
            }

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(value, zone.Id);
        }

#endif

#if NET20
        public static Month ToMonth(DateTime value)
#else
        public static Month ToMonth(this DateTime value)
#endif
        {
            return new Month(value);
        }

    }
}