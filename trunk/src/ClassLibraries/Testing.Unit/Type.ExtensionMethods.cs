namespace Cavity
{
    using System;
    using System.Linq;

    public static class TypeExtensionMethods
    {
        /// <summary>
        /// Returns <see langword="true"/> if the specified <paramref name="type"/>
        /// implements the specified <paramref name="interface"/>;
        /// otherwise <see langword="false"/>.
        /// </summary>
        /// <param name="type">The type to assess.</param>
        /// <param name="interface">The expected interface type.</param>
        /// <returns>
        /// An indicator if the <paramref name="type"/> is a static class
        /// implements the specified <paramref name="interface"/>.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/library/87d83y5b">interface (C# Reference)</seealso>
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

            return type
                .GetInterfaces()
                .Any(item => item.FullName.Equals(@interface.FullName));
        }

        /// <summary>
        /// Returns <see langword="true"/> if the specified <paramref name="type"/> is a static class;
        /// otherwise <see langword="false"/>.
        /// </summary>
        /// <param name="type">The type to assess.</param>
        /// <returns>An indicator if the <paramref name="type"/> is a static class.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/79b3xss3">Static Classes and Static Class Members (C# Programming Guide)</seealso>
        public static bool IsStatic(this Type type)
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            return type.IsAbstract && type.IsSealed && type.IsSubclassOf(typeof(object));
        }
    }
}