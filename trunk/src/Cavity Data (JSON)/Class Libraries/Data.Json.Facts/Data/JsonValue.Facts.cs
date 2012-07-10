namespace Cavity.Data
{
    using Xunit;

    public sealed class JsonValueFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonValue>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }
    }
}