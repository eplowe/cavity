namespace Cavity
{
    public static class CharExtensionMethods
    {
#if NET20
        public static bool IsWhiteSpace(char value)
#else
        public static bool IsWhiteSpace(this char value)
#endif
        {
            return WhiteSpace.Characters.Contains(value);
        }
    }
}