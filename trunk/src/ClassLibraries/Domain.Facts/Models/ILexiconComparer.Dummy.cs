namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;

    public sealed class ILexiconComparerDummy : ILexiconComparer
    {
        int IComparer<string>.Compare(string x, string y)
        {
            throw new NotSupportedException();
        }

        string ILexiconComparer.Normalize(string value)
        {
            throw new NotSupportedException();
        }
    }
}