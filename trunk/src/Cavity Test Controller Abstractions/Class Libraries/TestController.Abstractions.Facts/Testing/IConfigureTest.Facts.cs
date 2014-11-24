namespace Cavity.Testing
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class IConfigureTestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IConfigureTest>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Configurations_get()
        {
            var expected = new List<string>();

            var mock = new Mock<IConfigureTest>();
            mock
                .SetupGet(x => x.Configurations)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Configurations;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}