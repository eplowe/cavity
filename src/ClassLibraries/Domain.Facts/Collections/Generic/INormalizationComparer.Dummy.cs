namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    public sealed class NormalizationComparerDummy : INormalizationComparer
    {
        int IComparer<string>.Compare(string x,
                                      string y)
        {
            throw new NotSupportedException();
        }

        string INormalizationComparer.Normalize(string value)
        {
            throw new NotSupportedException();
        }
    }
}