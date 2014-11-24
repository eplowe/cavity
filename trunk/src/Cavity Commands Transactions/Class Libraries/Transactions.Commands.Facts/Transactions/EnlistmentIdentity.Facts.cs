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
        public void opEquality_EnlistmentIdentity_EnlistmentIdentity()
        {
            var obj = new EnlistmentIdentity(Guid.NewGuid());
            var comparand = new EnlistmentIdentity(Guid.NewGuid());

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opInequality_EnlistmentIdentity_EnlistmentIdentity()
        {
            var obj = new EnlistmentIdentity(Guid.NewGuid());
            var comparand = new EnlistmentIdentity(Guid.NewGuid());

            Assert.True(obj != comparand);
        }

        [Fact]
        public void op_Equals()
        {
            var obj = new EnlistmentIdentity(Guid.NewGuid());
            var comparand = new EnlistmentIdentity(Guid.NewGuid());

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_whenDifferentType()
        {
            var obj = new EnlistmentIdentity(Guid.NewGuid());

            Assert.False(obj.Equals(new object()));
        }

        [Fact]
        public void op_Equals_whenSame()
        {
            var obj = new EnlistmentIdentity(Guid.NewGuid());

            // ReSharper disable EqualExpressionComparison
            Assert.True(obj.Equals(obj));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_GetHashCode()
        {
            var actual = new EnlistmentIdentity(Guid.NewGuid()).GetHashCode();

            Assert.NotEqual(0, actual);
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