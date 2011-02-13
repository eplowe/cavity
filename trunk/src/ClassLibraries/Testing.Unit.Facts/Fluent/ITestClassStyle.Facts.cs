namespace Cavity.Fluent
{
    using Moq;
    using Xunit;

    public sealed class ITestClassStyleFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestClassStyle).IsInterface);
        }

        [Fact]
        public void op_IsAbstractBaseClass()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestClassStyle>();
            mock
                .Setup(x => x.IsAbstractBaseClass())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsAbstractBaseClass();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IsConcreteClass()
        {
            var expected = new Mock<ITestClassSealed>().Object;

            var mock = new Mock<ITestClassStyle>();
            mock
                .Setup(x => x.IsConcreteClass())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsConcreteClass();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}