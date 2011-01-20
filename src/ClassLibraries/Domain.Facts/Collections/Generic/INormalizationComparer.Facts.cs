namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class INormalizationComparerFacts
    {
        [Fact]
        public void INormalizationComparer_Normalize_string()
        {
            try
            {
                var value = (new NormalizationComparerDummy() as INormalizationComparer).Normalize(null);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<INormalizationComparer>()
                            .IsInterface()
                            .Implements<IComparer<string>>()
                            .Result);
        }
    }
}