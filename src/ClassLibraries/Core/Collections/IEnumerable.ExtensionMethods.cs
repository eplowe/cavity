namespace Cavity.Collections
{
    using System.Collections;
    using System.Linq;

    public static class IEnumerableExtensionMethods
    {
        public static bool IsNullOrEmpty(this IEnumerable obj)
        {
            return null == obj || !obj.Cast<object>().Any();
        }
    }
}