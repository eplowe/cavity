namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public sealed class UnityServiceLocator : ServiceLocatorImplBase
    {
        private readonly IUnityContainer _container;

        public UnityServiceLocator(IUnityContainer container)
        {
            if (null == container)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType,
                                                string key)
        {
            return _container.Resolve(serviceType, key);
        }
    }
}