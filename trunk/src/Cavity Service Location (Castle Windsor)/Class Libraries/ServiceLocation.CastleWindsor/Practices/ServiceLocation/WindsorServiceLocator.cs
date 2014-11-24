namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Collections.Generic;
    using Castle.Windsor;
    using Microsoft.Practices.ServiceLocation;

    public sealed class WindsorServiceLocator : ServiceLocatorImplBase
    {
        private readonly IWindsorContainer _container;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            if (null == container)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (object[])_container.ResolveAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType,
                                                string key)
        {
            return null == key
                       ? _container.Resolve(serviceType)
                       : _container.Resolve(key, serviceType);
        }
    }
}