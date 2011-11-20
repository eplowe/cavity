namespace Cavity.Net
{
    using System.Diagnostics.CodeAnalysis;

    public sealed class Host : ComparableObject
    {
        public Host(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Host names are canonically lower case.")]
        public override string ToString()
        {
            return "{0}".FormatWith(Name).ToLowerInvariant();
        }
    }
}