namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public class ITestClassStyleFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestClassStyle).IsInterface);
        }

        [Fact]
        public void ITestClassStyle_IsAbstractBaseClass()
        {
            try
            {
                ITestType value = (new ITestClassStyleDummy() as ITestClassStyle).IsAbstractBaseClass();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestClassStyle_IsConcreteClass()
        {
            try
            {
                ITestClassSealed value = (new ITestClassStyleDummy() as ITestClassStyle).IsConcreteClass();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}