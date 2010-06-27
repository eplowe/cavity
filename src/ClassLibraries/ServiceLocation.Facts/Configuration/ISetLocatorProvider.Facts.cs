﻿namespace Cavity.Configuration
{
    using System;
    using Xunit;

    public sealed class ISetLocatorProviderFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ISetLocatorProvider).IsInterface);
        }

        [Fact]
        public void ISetLocatorProvider_Configure()
        {
            try
            {
                (new ISetLocatorProviderDummy() as ISetLocatorProvider).Configure();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}