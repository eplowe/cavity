namespace Cavity.Collections
{
    using System.Collections.Generic;

    public interface INormalizationComparer : IComparer<string>
    {
        string Normalize(string value);
    }
}