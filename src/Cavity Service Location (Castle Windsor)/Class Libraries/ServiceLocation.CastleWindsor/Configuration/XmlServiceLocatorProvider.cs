namespace Cavity.Configuration
{
    using System.Diagnostics;

    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;

    using Cavity.Diagnostics;
    using Cavity.Practices.ServiceLocation;

    using Microsoft.Practices.ServiceLocation;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            var container = new WindsorContainer(new XmlInterpreter());
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}