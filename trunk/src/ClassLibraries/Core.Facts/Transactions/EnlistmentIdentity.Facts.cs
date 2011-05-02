namespace Cavity.Transactions
{
    using System;
    using Xunit;

    public sealed class EnlistmentIdentityFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<EnlistmentIdentity>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_Guid()
        {
            var expected = Guid.NewGuid();
            var actual = new EnlistmentIdentity(expected).ResourceManager;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Instance()
        {
            Assert.NotNull(new PropertyExpectations<EnlistmentIdentity>(p => p.Instance)
                               .TypeIs<Guid>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_ResourceManager()
        {
            Assert.NotNull(new PropertyExpectations<EnlistmentIdentity>(p => p.ResourceManager)
                               .TypeIs<Guid>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}