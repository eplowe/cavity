namespace Cavity.Configuration
{
    using System.Diagnostics;

    using Autofac;
    using Autofac.Configuration;

    using Cavity.Diagnostics;
    using Cavity.Practices.ServiceLocation;

    using Microsoft.Practices.ServiceLocation;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader());
            var locator = new AutofacServiceLocator(builder.Build());

            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}