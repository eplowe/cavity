namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;

    public static class ICollectionExtensionMethods
    {
#if NET20
        public static void Append<T>(ICollection<T> obj, 
                                     params T[] items)
#else
        public static void Append<T>(this ICollection<T> obj,
                                     params T[] items)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            foreach (var item in items)
            {
                obj.Add(item);
            }
        }

#if NET20
        public static bool NotContains<T>(ICollection<T> obj, 
                                          T item)
#else
        public static bool NotContains<T>(this ICollection<T> obj,
                                          T item)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return !obj.Contains(item);
        }

#if NET20
        public static bool TryAdd<T>(ICollection<T> obj, 
                                     T item)
#else
        public static bool TryAdd<T>(this ICollection<T> obj,
                                     T item)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (obj.Contains(item))
            {
                return false;
            }

            obj.Add(item);
            return true;
        }
    }
}