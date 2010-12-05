namespace Cavity.Data
{
    using System.Collections.Generic;
    using Cavity.Models;

    public interface IStoreLexicon
    {
        void Delete(Lexicon lexicon);

        Lexicon Load();

        Lexicon Load(IComparer<string> comparer);

        void Save(Lexicon lexicon);
    }
}