namespace Cavity
{
    using System;

    public static class DateTimeExtensionMethods
    {
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