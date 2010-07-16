namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestClassSealedFacts
    {
        [Fact]
        public void ITestClassSealed_IsSealed()
        {
            try
            {
                var value = (new ITestClassSealedDummy() as ITestClassSealed).IsSealed();
                Assert.NotNull(value);
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
                var value = (new ITestClassSealedDummy() as ITestClassSealed).IsUnsealed();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestClassSealed).IsInterface);
        }
    }
}