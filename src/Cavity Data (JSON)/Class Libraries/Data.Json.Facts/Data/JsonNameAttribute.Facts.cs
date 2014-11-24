namespace Cavity.Data
{
    using System;
    using Xunit;

    public sealed class JsonNameAttributeFacts
    {
        [JsonName("example")]
        public int Value { get; set; }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonNameAttribute>()
                            .DerivesFrom<Attribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Property, false, true)
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new JsonNameAttribute("Example"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonNameAttribute(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonNameAttribute(null));
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<JsonNameAttribute>(x => x.Name)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .Result);
        }
    }
}