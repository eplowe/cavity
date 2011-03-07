namespace Cavity.Data
{
    using Moq;
    using Xunit;

    public sealed class IRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRepository>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Delete_AbsoluteUri()
        {
            var urn = new AbsoluteUri("urn://example.com/path");

            var mock = new Mock<IRepository>();
            mock
                .Setup(x => x.Delete(urn))
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Delete(urn));

            mock.VerifyAll();
        }
    }
}