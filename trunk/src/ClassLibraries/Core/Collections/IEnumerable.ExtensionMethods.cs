namespace Cavity.Collections
{
    using System.Collections;
    using System.Collections.Generic;
#if NET20
    using System.Diagnostics.CodeAnalysis;
#endif
#if !NET20
    using System.Linq;
#endif
    using System.Text;

    public static class IEnumerableExtensionMethods
    {
#if NET20
        public static string Concat(IEnumerable<string> source,
                                    char separator)
#else
        public static string Concat(this IEnumerable<string> source,
                                    char separator)
#endif
        {
            if (null == source)
            {
                return null;
            }

#if NET20
            if (0 == Count(source))
#else
            if (0 == source.Count())
#endif
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();
            foreach (var item in source)
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(separator);
                }

                buffer.Append(item);
            }

            return buffer.ToString();
        }

#if NET20
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "item", Justification = "There is no other way to count the items.")]
        public static int Count(IEnumerable obj)
        {
            if (null == obj)
            {
                return 0;
            }

            var count = 0;
            foreach (var item in obj)
            {
                count++;
            }

            return count;
        }
#endif

#if NET20
        public static bool IsNullOrEmpty(IEnumerable obj)
        {
            if (null == obj)
            {
                return true;
            }

            foreach (var item in obj)
            {
                if (null != item)
                {
                    return false;
                }
            }

            return true;
        }
#else
        public static bool IsNullOrEmpty(this IEnumerable obj)
        {
            return null == obj || !obj.Cast<object>().Any();
        }
#endif
    }
}