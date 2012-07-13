namespace Cavity.Data
{
    using System;

    using Xunit;

    public sealed class JsonIgnoreAttributeFacts
    {
        [JsonIgnore]
        public int Value { get; set; }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonIgnoreAttribute>()
                            .DerivesFrom<Attribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Property, false, true)
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonIgnoreAttribute());
        }
    }
}