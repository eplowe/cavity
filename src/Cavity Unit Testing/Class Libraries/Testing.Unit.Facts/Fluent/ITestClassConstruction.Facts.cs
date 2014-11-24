namespace Cavity.Fluent
{
    using Moq;
    using Xunit;

    public sealed class ITestClassConstructionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITestClassConstruction>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_HasDefaultConstructor()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestClassConstruction>();
            mock
                .Setup(x => x.HasDefaultConstructor())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HasDefaultConstructor();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_NoDefaultConstructor()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestClassConstruction>();
            mock
                .Setup(x => x.NoDefaultConstructor())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.NoDefaultConstructor();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}