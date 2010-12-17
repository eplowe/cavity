namespace Cavity.Net
{
    using Xunit;

    public sealed class NoContentResponseFacts
    {
        [Fact(Skip = "TODO")]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<NoContentResponse>()
                            .DerivesFrom<Response>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new NoContentResponse());
        }
    }
}