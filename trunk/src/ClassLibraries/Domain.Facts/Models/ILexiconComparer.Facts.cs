namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class ILexiconComparerFacts
    {
        [Fact]
        public void ILexiconComparer_Normalize_string()
        {
            try
            {
                var value = (new ILexiconComparerDummy() as ILexiconComparer).Normalize(null);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ILexiconComparer>()
                            .IsInterface()
                            .Implements<IComparer<string>>()
                            .Result);
        }
    }
}