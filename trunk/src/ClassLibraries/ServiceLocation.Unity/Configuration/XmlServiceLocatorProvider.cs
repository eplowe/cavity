namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Diagnostics;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposing the container tears down the configuration.")]
        public void Configure()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            IUnityContainer container = new UnityContainer();
            ((UnityConfigurationSection)ConfigurationManager.GetSection("unity")).Configure(container, "container");

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}