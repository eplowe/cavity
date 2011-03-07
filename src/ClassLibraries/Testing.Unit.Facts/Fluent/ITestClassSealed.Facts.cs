namespace Cavity.Fluent
{
    using Moq;
    using Xunit;

    public sealed class ITestClassSealedFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITestClassSealed>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_IsSealed()
        {
            var expected = new Mock<ITestClassConstruction>().Object;

            var mock = new Mock<ITestClassSealed>();
            mock
                .Setup(x => x.IsSealed())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsSealed();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IsUnsealed()
        {
            var expected = new Mock<ITestClassConstruction>().Object;

            var mock = new Mock<ITestClassSealed>();
            mock
                .Setup(x => x.IsUnsealed())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsUnsealed();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}