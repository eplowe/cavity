namespace Cavity.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
#if NET20
    using System.Diagnostics.CodeAnalysis;
#endif
#if !NET20
    using System.Globalization;
    using System.Linq;
#endif
    using System.Text;

    public static class IEnumerableExtensionMethods
    {
#if NET20
        public static string Concat(IEnumerable<string> source, 
                                    char separator)
        {
            return Concat(source, separator.ToString());
        }
#else
        public static string Concat(this IEnumerable<string> source, 
                                    char separator)
        {
            return Concat(source, separator.ToString(CultureInfo.InvariantCulture));
        }

#endif

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
        public static T First<T>(IEnumerable<T> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            foreach (var item in items)
            {
                return item;
            }
        
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static T FirstOrDefault<T>(IEnumerable<T> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            T result = default(T);

            foreach (var item in items)
            {
                return item;
            }

            return result;
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
        public static bool IsNotEmpty(IEnumerable obj)
#else
        public static bool IsNotEmpty(this IEnumerable obj)
#endif
        {
            return !IsEmpty(obj);
        }

#if NET20
        public static T Last<T>(IEnumerable<T> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            T result = default(T);

            foreach (var item in items)
            {
                result = item;
            }

            if (null == result)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            return result;
        }

        public static T LastOrDefault<T>(IEnumerable<T> items)
        {
            if (null == items)
            {
                throw new ArgumentNullException("items");
            }

            T result = default(T);

            foreach (var item in items)
            {
                result = item;
            }

            return result;
        }
#endif

#if NET20
        public static IEnumerable<T> Reverse<T>(IEnumerable<T> obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var list = new List<T>();
            foreach (var item in obj)
            {
                list.Insert(0, item);
            }

            return list;
        }
#endif

#if !NET20
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var result = new HashSet<T>();

            foreach (var item in obj)
            {
                result.Add(item);
            }

            return result;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> obj, 
                                              IEqualityComparer<T> comparer)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var result = new HashSet<T>(comparer);

            foreach (var item in obj)
            {
                result.Add(item);
            }

            return result;
        }

#endif

#if NET20
        public static IList<T> ToList<T>(IEnumerable<T> obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var list = new List<T>();
            foreach (var item in obj)
            {
                list.Add(item);
            }

            return list;
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