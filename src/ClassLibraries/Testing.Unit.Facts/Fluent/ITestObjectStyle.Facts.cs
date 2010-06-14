namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public class ITestObjectStyleFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestObjectStyle).IsInterface);
        }

        [Fact]
        public void ITestObjectStyle_IsAbstractBaseClass()
        {
            try
            {
                ITestObject value = (new ITestObjectStyleDummy() as ITestObjectStyle).IsAbstractBaseClass();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}