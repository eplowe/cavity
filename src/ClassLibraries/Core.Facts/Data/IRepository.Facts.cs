namespace Cavity.Data
{
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
    }
}