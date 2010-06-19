namespace Cavity.Configuration
{
    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using CommonServiceLocator.WindsorAdapter;
    using Microsoft.Practices.ServiceLocation;

    public sealed class XmlServiceLocatorProvider : ISetLocatorProvider
    {
        public void Configure()
        {
            var container = new WindsorContainer(new XmlInterpreter());
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}