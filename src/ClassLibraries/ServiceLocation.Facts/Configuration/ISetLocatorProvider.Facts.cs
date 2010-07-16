namespace Cavity.Configuration
{
    using System;
    using Xunit;

    public sealed class ISetLocatorProviderFacts
    {
        [Fact]
        public void ISetLocatorProvider_Configure()
        {
            try
            {
                new ISetLocatorProviderDummy().Configure();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ISetLocatorProvider).IsInterface);
        }
    }
}