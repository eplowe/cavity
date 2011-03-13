namespace Cavity.Data
{
    using Moq;
    using Xunit;

    public sealed class IEntityFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IEntity>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_ToEntity()
        {
            const string expected = "123";

            var mock = new Mock<IEntity>();
            mock
                .Setup(x => x.ToEntity())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ToEntity();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}