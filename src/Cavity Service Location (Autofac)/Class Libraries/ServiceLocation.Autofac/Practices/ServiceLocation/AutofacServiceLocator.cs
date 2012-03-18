namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using Microsoft.Practices.ServiceLocation;

    public sealed class AutofacServiceLocator : ServiceLocatorImplBase
    {
        private readonly IComponentContext _container;

        public AutofacServiceLocator(IComponentContext container)
        {
            if (null == container)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var instance = _container.Resolve(enumerableType);

            return ((IEnumerable)instance).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, 
                                                string key)
        {
            return null == key
                       ? _container.Resolve(serviceType)
                       : _container.ResolveNamed(key, serviceType);
        }
    }
}