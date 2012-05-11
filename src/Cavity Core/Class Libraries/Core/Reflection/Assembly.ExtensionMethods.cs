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

            return ToDirectory(obj.Location);
        }

        public static DirectoryInfo ToDirectory(string location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            location = location.Trim();
            if (0 == location.Length)
            {
                throw new ArgumentOutOfRangeException("location");
            }

            var file = new FileInfo(location);
            if (null == file.Directory)
            {
                throw new DirectoryNotFoundException(location);
            }

            return new DirectoryInfo(file.Directory.FullName);
        }
    }
}