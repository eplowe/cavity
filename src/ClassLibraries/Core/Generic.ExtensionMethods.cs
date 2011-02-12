namespace Cavity
{
    using System.Linq;

    public static class GenericExtensionMethods
    {
        public static bool In<T>(this T value,
                                 params T[] args)
        {
            return args.Contains(value);
        }
    }
}