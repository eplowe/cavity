namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using Cavity.Models;

    public sealed class IStoreLexiconDummy : IStoreLexicon
    {
        void IStoreLexicon.Delete(Lexicon lexicon)
        {
            throw new NotSupportedException();
        }

        Lexicon IStoreLexicon.Load()
        {
            throw new NotSupportedException();
        }

        Lexicon IStoreLexicon.Load(IComparer<string> comparer)
        {
            throw new NotSupportedException();
        }

        void IStoreLexicon.Save(Lexicon lexicon)
        {
            throw new NotSupportedException();
        }
    }
}