namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestClassConstructionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestClassConstruction).IsInterface);
        }

        [Fact]
        public void ITestClassConstruction_HasDefaultConstructor()
        {
            try
            {
                var value = (new ITestClassConstructionDummy() as ITestClassConstruction).HasDefaultConstructor();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestClassConstruction_NoDefaultConstructor()
        {
            try
            {
                var value = (new ITestClassConstructionDummy() as ITestClassConstruction).NoDefaultConstructor();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}