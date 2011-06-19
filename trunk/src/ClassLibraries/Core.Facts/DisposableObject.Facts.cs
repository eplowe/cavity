namespace Cavity
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class DisposableObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DisposableObject>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Implements<IDisposable>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var obj = new DerivedDisposableObject())
            {
                Assert.IsAssignableFrom<DisposableObject>(obj);
            }
        }
    }
}