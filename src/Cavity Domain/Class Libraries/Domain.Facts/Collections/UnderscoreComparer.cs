namespace Cavity.Collections
{
    using System;

    public sealed class UnderscoreComparer : NormalityComparer
    {
        public override string Normalize(string value)
        {
            return value.ReplaceAllWith(string.Empty, StringComparison.Ordinal, "_");
        }
    }
}