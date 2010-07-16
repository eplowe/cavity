namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestExpectationFacts
    {
        [Fact]
        public void ITestExpectation_Check()
        {
            try
            {
                var value = (new ITestExpectationDummy() as ITestExpectation).Check();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestExpectation).IsInterface);
        }
    }
}