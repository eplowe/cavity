namespace Cavity.Collections
{
    using System;
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
            return Concat(source, separator.ToString());
        }

#if NET20
        public static string Concat(IEnumerable<string> source,
                                    string separator)
#else
        public static string Concat(this IEnumerable<string> source,
                                    string separator)
#endif
        {
            if (null == source)
            {
                return null;
            }

            if (null == separator)
            {
                throw new ArgumentNullException("separator");
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
        public static bool IsEmpty(IEnumerable obj)
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
        public static bool IsEmpty(this IEnumerable obj)
        {
            return null == obj || !obj.Cast<object>().Any();
        }
#endif

#if NET20
        public static Queue<T> ToQueue<T>(IEnumerable<T> obj)
#else
        public static Queue<T> ToQueue<T>(this IEnumerable<T> obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var result = new Queue<T>();

            foreach (var item in obj)
            {
                result.Enqueue(item);
            }

            return result;
        }

#if NET20
        public static Stack<T> ToStack<T>(IEnumerable<T> obj)
#else
        public static Stack<T> ToStack<T>(this IEnumerable<T> obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var result = new Stack<T>();

            foreach (var item in obj)
            {
                result.Push(item);
            }

            return result;
        }
    }
}