namespace Cavity.Configuration
{
    using System.Diagnostics;
    using Autofac;
    using Autofac.Configuration;
    using AutofacContrib.CommonServiceLocator;
    using Cavity.Diagnostics;
    using Microsoft.Practices.ServiceLocation;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader());
            var locator = new AutofacServiceLocator(builder.Build());

            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}