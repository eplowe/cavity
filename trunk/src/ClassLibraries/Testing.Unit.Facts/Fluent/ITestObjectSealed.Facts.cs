namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public class ITestObjectSealedFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestObjectSealed).IsInterface);
        }

        [Fact]
        public void ITestObjectSealed_IsSealed()
        {
            try
            {
                ITestObjectConstruction value = (new ITestObjectSealedDummy() as ITestObjectSealed).IsSealed();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObjectSealed_IsUnsealed()
        {
            try
            {
                ITestObjectConstruction value = (new ITestObjectSealedDummy() as ITestObjectSealed).IsUnsealed();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}