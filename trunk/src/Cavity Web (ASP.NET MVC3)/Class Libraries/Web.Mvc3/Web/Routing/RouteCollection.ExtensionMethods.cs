namespace Cavity.Web.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteCollectionExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "A non-generic overload has been provided.")]
        public static void Register<T>(this RouteCollection routes)
            where T : IRegisterRoutes
        {
            var instance = (IRegisterRoutes)Activator.CreateInstance(typeof(T));
            instance.Register(routes);
        }

        public static void Register(this RouteCollection routes, 
                                    IEnumerable<Type> types)
        {
            if (null == types)
            {
                throw new ArgumentNullException("types");
            }

            foreach (var type in types)
            {
                routes.Register(type);
            }
        }

        public static void Register(this RouteCollection routes, 
                                    Type type)
        {
            routes.Register(type, false);
        }

        public static void Register(this RouteCollection routes, 
                                    Type type, 
                                    bool skipInterface)
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsClass)
            {
                return;
            }

            if (!type.IsSubclassOf(typeof(Controller)))
            {
                return;
            }

            if (skipInterface || null == type.GetInterface("IRegisterRoutes"))
            {
                return;
            }

            var instance = (IRegisterRoutes)Activator.CreateInstance(type);
            instance.Register(routes);
        }
    }
}