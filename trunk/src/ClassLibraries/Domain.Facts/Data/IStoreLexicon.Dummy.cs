namespace Cavity.Data
{
    using System;
    using Cavity.Models;

    public sealed class IStoreLexiconDummy : IStoreLexicon
    {
        void IStoreLexicon.Delete(Lexicon lexicon)
        {
            throw new NotSupportedException();
        }

        Lexicon IStoreLexicon.Load(ILexiconComparer comparer)
        {
            throw new NotSupportedException();
        }

        void IStoreLexicon.Save(Lexicon lexicon)
        {
            throw new NotSupportedException();
        }
    }
}