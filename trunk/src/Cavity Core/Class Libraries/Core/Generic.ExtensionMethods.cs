namespace Cavity
{
#if !NET20
    using System.Linq;

#endif

    public static class GenericExtensionMethods
    {
#if NET20
        public static bool In<T>(T value,
                                 params T[] args)
#else
        public static bool In<T>(this T value,
                                 params T[] args)
#endif
        {
#if NET20
            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.Equals(value))
                {
                    return true;
                }
            }

            return false;
#else
            return args.Contains(value);
#endif
        }
    }
}