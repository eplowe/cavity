namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public class ITestObjectConstructionFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestObjectConstruction).IsInterface);
        }

        [Fact]
        public void ITestObjectConstruction_HasDefaultConstructor()
        {
            try
            {
                ITestObject value = (new ITestObjectConstructionDummy() as ITestObjectConstruction).HasDefaultConstructor();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObjectConstruction_NoDefaultConstructor()
        {
            try
            {
                ITestObject value = (new ITestObjectConstructionDummy() as ITestObjectConstruction).NoDefaultConstructor();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}