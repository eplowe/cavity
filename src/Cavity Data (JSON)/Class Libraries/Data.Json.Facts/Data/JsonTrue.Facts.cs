namespace Cavity.Data
{
    using Xunit;

    public sealed class JsonTrueFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonTrue>()
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
            Assert.NotNull(new JsonTrue());
        }
    }
}