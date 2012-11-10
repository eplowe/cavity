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
    }
}