namespace Cavity
{
    public static class TemporalExtensionMethods
    {
#if NET20
        public static Date ToDate(string obj)
#else
        public static Date ToDate(this string obj)
#endif
        {
            return Date.FromString(obj);
        }

#if NET20
        public static Month ToMonth(string obj)
#else
        public static Month ToMonth(this string obj)
#endif
        {
            return Month.FromString(obj);
        }

#if NET20
        public static Quarter ToQuarter(string obj)
#else
        public static Quarter ToQuarter(this string obj)
#endif
        {
            return Quarter.FromString(obj);
        }
    }
}