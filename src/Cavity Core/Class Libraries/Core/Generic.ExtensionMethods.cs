namespace Cavity
{
    using System;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Properties;

    public static class GenericExtensionMethods
    {
#if NET20
        public static bool EqualsNoneOf<T>(T obj, 
                                           params T[] args)
#else
        public static bool EqualsNoneOf<T>(this T obj,
                                           params T[] args)
#endif
        {
            return !EqualsOneOf(obj, args);
        }

#if NET20
        public static bool EqualsOneOf<T>(T obj, 
                                          params T[] args)
#else
        public static bool EqualsOneOf<T>(this T obj,
                                          params T[] args)
#endif
        {
#if NET20
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.Equals(obj))
                {
                    return true;
                }
            }

            return false;
#else
            return args.Contains(obj);
#endif
        }

#if NET20
        public static bool In<T>(T value, 
                                 params T[] args)
#else
        public static bool In<T>(this T value,
                                 params T[] args)
#endif
        {
            return EqualsOneOf(value, args);
        }

#if NET20
        public static bool Is<T>(T value, 
                                 T comparand)
#else
        public static bool Is<T>(this T value,
                                 T comparand)
#endif
        {
            return ReferenceEquals(null, value)
                       ? ReferenceEquals(null, comparand)
                       : value.Equals(comparand);
        }

#if NET20
        public static bool IsBoundedBy<T>(T obj, 
                                          T lower, 
                                          T upper)
#else

        public static bool IsBoundedBy<T>(this T obj,
                                          T lower,
                                          T upper)
#endif
            where T : IComparable<T>
        {
            if (ReferenceEquals(null, upper))
            {
                throw new ArgumentNullException("upper");
            }

            if (1 > upper.CompareTo(lower))
            {
                throw new ArgumentException(Resources.ObjectExtensionMethods_IsBoundedBy_Message);
            }

            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return -1 < obj.CompareTo(lower) && 1 > obj.CompareTo(upper);
        }

#if NET20
        public static bool IsGreaterThan<T>(T value, 
                                            T comparand)
#else
        public static bool IsGreaterThan<T>(this T value,
                                            T comparand)
#endif
            where T : IComparable<T>
        {
            return value.CompareTo(comparand) > 0;
        }

#if NET20
        public static bool IsLessThan<T>(T value, 
                                         T comparand)
#else
        public static bool IsLessThan<T>(this T value,
                                         T comparand)
#endif
            where T : IComparable<T>
        {
            return 0 > value.CompareTo(comparand);
        }

#if NET20
        public static bool IsNot<T>(T value, 
                                    T comparand)
#else
        public static bool IsNot<T>(this T value,
                                    T comparand)
#endif
        {
            return !Is(value, comparand);
        }

#if NET20
        public static bool IsNotBoundedBy<T>(T obj, 
                                             T lower, 
                                             T upper)
#else

        public static bool IsNotBoundedBy<T>(this T obj,
                                             T lower,
                                             T upper)
#endif
            where T : IComparable<T>
        {
            return !IsBoundedBy(obj, lower, upper);
        }

#if NET20
        public static bool NotIn<T>(T value, 
                                    params T[] args)
#else
        public static bool NotIn<T>(this T value,
                                    params T[] args)
#endif
        {
            return !In(value, args);
        }
    }
}