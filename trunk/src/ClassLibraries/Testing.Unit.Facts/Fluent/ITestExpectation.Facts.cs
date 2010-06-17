namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestExpectationFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ITestExpectation).IsInterface);
        }

        [Fact]
        public void ITestExpectation_Check()
        {
            try
            {
                bool value = (new ITestExpectationDummy() as ITestExpectation).Check();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}