namespace Cavity.Data
{
    using Xunit;

    public sealed class JsonNullFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonNull>()
                            .DerivesFrom<JsonValue>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonNull());
        }
    }
}