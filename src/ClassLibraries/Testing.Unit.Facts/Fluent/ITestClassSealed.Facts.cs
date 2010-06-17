namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestClassSealedFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ITestClassSealed).IsInterface);
        }

        [Fact]
        public void ITestClassSealed_IsSealed()
        {
            try
            {
                ITestClassConstruction value = (new ITestClassSealedDummy() as ITestClassSealed).IsSealed();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestClassSealed_IsUnsealed()
        {
            try
            {
                ITestClassConstruction value = (new ITestClassSealedDummy() as ITestClassSealed).IsUnsealed();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}