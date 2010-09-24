namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class ITestHttpExpectationFacts
    {
        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            try
            {
                Response response = null;
                var value = (new ITestHttpExpectationDummy() as ITestHttpExpectation).Check(response);
                Assert.True(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestHttpExpectation).IsInterface);
        }
    }
}