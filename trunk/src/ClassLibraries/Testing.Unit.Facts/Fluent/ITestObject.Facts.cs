namespace Cavity.Fluent
{
    using System;
    using Cavity.Types;
    using Xunit;

    public class ITestObjectFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestObject).IsInterface);
        }

        [Fact]
        public void ITestObject_Result_get()
        {
            try
            {
                bool value = (new ITestObjectDummy() as ITestObject).Result;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_ImplementsOfT()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).Implements<Interface1>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}