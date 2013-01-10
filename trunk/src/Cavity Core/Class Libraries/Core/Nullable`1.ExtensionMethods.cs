namespace Cavity
{
    public static class NullableOfTExtensionMethods
    {
#if NET20
        public static bool HasNoValue<T>(T? obj)
#else
        public static bool HasNoValue<T>(this T? obj)
#endif
            where T : struct 
        {
            return !obj.HasValue;
        }
    }
}