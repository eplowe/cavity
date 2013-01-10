namespace Cavity.Text
{
    using System;
    using System.Text;

    public static class StringBuilderExtensionMethods
    {
#if NET20
        public static bool ContainsText(StringBuilder obj)
#else
        public static bool ContainsText(this StringBuilder obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return IsNotEmpty(obj);
        }

#if NET20
        public static bool IsEmpty(StringBuilder obj)
#else
        public static bool IsEmpty(this StringBuilder obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return 0 == obj.Length;
        }

#if NET20
        public static bool IsNotEmpty(StringBuilder obj)
#else
        public static bool IsNotEmpty(this StringBuilder obj)
#endif
        {
            return !IsEmpty(obj);
        }
    }
}