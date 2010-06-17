namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestClassConstructionFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ITestClassConstruction).IsInterface);
        }

        [Fact]
        public void ITestClassConstruction_HasDefaultConstructor()
        {
            try
            {
                ITestType value = (new ITestClassConstructionDummy() as ITestClassConstruction).HasDefaultConstructor();
                Assert.True(false);
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
                ITestType value = (new ITestClassConstructionDummy() as ITestClassConstruction).NoDefaultConstructor();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}