namespace Cavity.Collections
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class INormalizationComparerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<INormalizationComparer>()
                            .IsInterface()
                            .Implements<IComparer<string>>()
                            .Result);
        }

        [Fact]
        public void op_Normalize_string()
        {
            const string expected = "Example";

            var mock = new Mock<INormalizationComparer>();
            mock
                .Setup(x => x.Normalize(expected))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Normalize(expected);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}