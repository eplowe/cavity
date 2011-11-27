namespace Cavity.Reflection
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class AssemblyExtensionMethods
    {
#if NET20
        public static DirectoryInfo Directory(Assembly obj)
#else
        public static DirectoryInfo Directory(this Assembly obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var location = new FileInfo(obj.Location);
            if (null == location.Directory)
            {
                throw new DirectoryNotFoundException(obj.Location);
            }

            return new DirectoryInfo(location.Directory.FullName);
        }
    }
}