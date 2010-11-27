namespace Cavity
{
    public static class CharExtensionMethods
    {
        public static bool IsWhiteSpace(this char value)
        {
            return WhiteSpace.Characters.Contains(value);
        }
    }
}