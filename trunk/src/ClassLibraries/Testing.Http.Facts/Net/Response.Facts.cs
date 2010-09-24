namespace Cavity.Net
{
    using Xunit;

    public sealed class ResponseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Response>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Response());
        }
    }
}