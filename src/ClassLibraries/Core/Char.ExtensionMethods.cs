namespace Cavity
{
    using System.Linq;

    public static class CharExtensionMethods
    {
        public static bool IsIn(this char value,
                                params char[] args)
        {
            return args.Contains(value);
        }

        public static bool IsWhiteSpace(this char value)
        {
            return WhiteSpace.Characters.Contains(value);
        }
    }
}