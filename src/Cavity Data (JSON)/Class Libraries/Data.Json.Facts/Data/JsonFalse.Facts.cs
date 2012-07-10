namespace Cavity.Data
{
    using Xunit;

    public sealed class JsonFalseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonFalse>()
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
            Assert.NotNull(new JsonFalse());
        }
    }
}