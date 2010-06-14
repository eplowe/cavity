namespace Cavity
{
    using System;

    public static class TypeExtensionMethods
    {
        public static bool Implements(this Type type, Type @interface)
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }
            else if (null == @interface)
            {
                throw new ArgumentNullException("interface");
            }

            bool result = false;
            foreach (var item in type.GetInterfaces())
            {
                if (item.FullName.Equals(@interface.FullName))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}