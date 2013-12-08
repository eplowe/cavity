namespace Cavity
{
    using Cavity.Models;

    public static class StringExtensionMethods
    {
        public static BritishPostcode ToPostcode(this string obj)
        {
            return BritishPostcode.FromString(obj);
        }
    }
}