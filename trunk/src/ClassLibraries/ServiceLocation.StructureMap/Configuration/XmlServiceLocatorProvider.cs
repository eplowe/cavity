namespace Cavity.Configuration
{
    using Microsoft.Practices.ServiceLocation;
    using StructureMap;
    using StructureMap.ServiceLocatorAdapter;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            ObjectFactory.Initialize(x => x.UseDefaultStructureMapConfigFile = true);

            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(ObjectFactory.Container));
        }
    }
}