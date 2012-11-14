namespace Cavity.Collections
{
    using System;
    using System.Collections.Generic;

    public static class IListExtensionMethods
    {
#if NET20
        public static IList<T> RemoveLast<T>(IList<T> obj, 
                                             int count)
#else
        public static IList<T> RemoveLast<T>(this IList<T> obj,
                                             int count)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 > count)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (count >= obj.Count)
            {
                obj.Clear();
                return obj;
            }

            for (var i = 0; i < count; i++)
            {
                obj.Remove(obj[obj.Count - 1]);
            }

            return obj;
        }
    }
}