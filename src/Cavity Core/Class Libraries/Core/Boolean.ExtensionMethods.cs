namespace Cavity
{
    public static class BooleanExtensionMethods
    {
#if NET20
        public static bool And(bool value,
                               bool comparand)
#else
        public static bool And(this bool value,
                               bool comparand)
#endif
        {
            return value && comparand;
        }

#if NET20
        public static bool IsFalse(bool value)
#else
        public static bool IsFalse(this bool value)
#endif
        {
            return !value;
        }

#if NET20
        public static bool IsTrue(bool value)
#else
        public static bool IsTrue(this bool value)
#endif
        {
            return value;
        }

#if NET20
        public static bool Or(bool value,
                              bool comparand)
#else
        public static bool Or(this bool value,
                              bool comparand)
#endif
        {
            return value || comparand;
        }
    }
}