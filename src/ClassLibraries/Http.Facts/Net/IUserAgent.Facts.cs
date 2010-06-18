namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IUserAgentFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IUserAgent).IsInterface);
        }

        [Fact]
        public void IUserAgent_Value_get()
        {
            try
            {
                string value = (new IUserAgentDummy() as IUserAgent).Value;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}