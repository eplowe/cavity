namespace Cavity.Configuration
{
    using System.Diagnostics;
    using Cavity.Diagnostics;
    using Microsoft.Practices.ServiceLocation;
    using StructureMap;
    using StructureMap.ServiceLocatorAdapter;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            ObjectFactory.Initialize(x => x.UseDefaultStructureMapConfigFile = true);

            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(ObjectFactory.Container));
        }
    }
}