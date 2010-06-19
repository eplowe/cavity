namespace Cavity.Configuration
{
    using System;

    public sealed class ISetLocatorProviderDummy : ISetLocatorProvider
    {
        public void Configure()
        {
            throw new NotSupportedException();
        }
    }
}