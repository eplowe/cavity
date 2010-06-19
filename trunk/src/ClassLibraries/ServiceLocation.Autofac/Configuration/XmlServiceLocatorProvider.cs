namespace Cavity.Configuration
{
    using Autofac;
    using Autofac.Configuration;
    using AutofacContrib.CommonServiceLocator;
    using Microsoft.Practices.ServiceLocation;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader());
            var locator = new AutofacServiceLocator((IComponentContext)builder.Build());

            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}