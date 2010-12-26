namespace Cavity.Data
{
    using Cavity.Models;

    public interface IStoreLexicon
    {
        void Delete(Lexicon lexicon);

        Lexicon Load(ILexiconComparer comparer);

        void Save(Lexicon lexicon);
    }
}