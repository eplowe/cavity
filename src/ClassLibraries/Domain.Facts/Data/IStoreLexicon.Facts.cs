namespace Cavity.Data
{
    using System;
    using Cavity.Models;
    using Xunit;

    public sealed class IStoreLexiconFacts
    {
        [Fact]
        public void IStoreLexicon_Load()
        {
            try
            {
                var value = (new IStoreLexiconDummy() as IStoreLexicon).Load();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IStoreLexicon_Load_IComparerOfString()
        {
            try
            {
                var value = (new IStoreLexiconDummy() as IStoreLexicon).Load(StringComparer.Ordinal);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IStoreLexicon_Save_Lexicon()
        {
            try
            {
                (new IStoreLexiconDummy() as IStoreLexicon).Save(new Lexicon());
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IStoreLexicon>()
                            .IsInterface()
                            .Result);
        }
    }
}