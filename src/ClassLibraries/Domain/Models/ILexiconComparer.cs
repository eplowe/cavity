namespace Cavity.Models
{
    using System.Collections.Generic;

    public interface ILexiconComparer : IComparer<string>
    {
        string Normalize(string value);
    }
}