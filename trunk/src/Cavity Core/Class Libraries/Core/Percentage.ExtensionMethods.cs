namespace Cavity
{
    using System;
    using System.Globalization;

    public static class PercentageExtensionMethods
    {
#if NET20
        public static decimal Percent<T>(decimal value,
                                         T total)
#else
        public static decimal Percent<T>(this decimal value,
                                         T total)
#endif
        {
            return CalculatePercent(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal Percent<T>(double value,
                                         T total)
#else
        public static decimal Percent<T>(this double value,
                                         T total)
#endif
        {
            return CalculatePercent((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal Percent<T>(float value,
                                         T total)
#else
        public static decimal Percent<T>(this float value,
                                         T total)
#endif
        {
            return CalculatePercent((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal Percent<T>(int value,
                                         T total)
#else
        public static decimal Percent<T>(this int value,
                                         T total)
#endif
        {
            return CalculatePercent(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal Percent<T>(long value,
                                         T total)
#else
        public static decimal Percent<T>(this long value,
                                         T total)
#endif
        {
            return CalculatePercent(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal Percent<T>(short value,
                                         T total)
#else
        public static decimal Percent<T>(this short value,
                                         T total)
#endif
        {
            return CalculatePercent(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(decimal value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this decimal value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(double value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this double value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(float value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this float value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(int value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this int value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(long value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this long value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageChange<T>(short value,
                                              T total)
#else
        public static decimal PercentageChange<T>(this short value,
                                                  T total)
#endif
        {
            return CalculatePercentageChange(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(decimal value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this decimal value,
                                              T total)
#endif
        {
            return CalculatePercentageOf(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(double value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this double value,
                                              T total)
#endif
        {
            return CalculatePercentageOf((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(float value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this float value,
                                              T total)
#endif
        {
            return CalculatePercentageOf((decimal)value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(int value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this int value,
                                              T total)
#endif
        {
            return CalculatePercentageOf(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(long value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this long value,
                                              T total)
#endif
        {
            return CalculatePercentageOf(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

#if NET20
        public static decimal PercentageOf<T>(short value,
                                              T total)
#else
        public static decimal PercentageOf<T>(this short value,
                                              T total)
#endif
        {
            return CalculatePercentageOf(value, (decimal)Convert.ChangeType(total, typeof(decimal), CultureInfo.InvariantCulture));
        }

        private static decimal CalculatePercent(decimal value, decimal total)
        {
            if (0 == total)
            {
                return 0;
            }

            return value / total * 100;
        }

        private static decimal CalculatePercentageChange(decimal before, decimal after)
        {
            if (0 == before)
            {
                return 0;
            }

            return 100 * (after - before) / Math.Abs(before);
        }

        private static decimal CalculatePercentageOf(decimal value, decimal total)
        {
            if (0 == value)
            {
                return 0;
            }

            return total / 100 * value;
        }
    }
}