namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class NoContentResponseFacts
    {
        [Fact]
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