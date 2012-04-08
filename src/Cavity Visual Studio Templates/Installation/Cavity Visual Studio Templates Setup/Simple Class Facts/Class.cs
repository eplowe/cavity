namespace $rootnamespace$
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class $safeitemrootname$Facts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<$safeitemrootname$>()
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
            Assert.NotNull(new $safeitemrootname$());
        }
    }
}