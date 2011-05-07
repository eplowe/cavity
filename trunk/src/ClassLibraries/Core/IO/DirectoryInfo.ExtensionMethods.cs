namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class DirectoryInfoExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static DirectoryInfo ToDirectory(this DirectoryInfo obj,
                                                object name)
        {
            return obj.ToDirectory(name, false);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static DirectoryInfo ToDirectory(this DirectoryInfo obj,
                                                object name,
                                                bool create)
        {
            var dir = new DirectoryInfo(PathCombine(obj, name));
            if (create && !dir.Exists)
            {
                dir.Create();
                dir.Refresh();
            }

            return dir;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static FileInfo ToFile(this DirectoryInfo obj,
                                      object name)
        {
            return new FileInfo(PathCombine(obj, name));
        }

        private static string PathCombine(this FileSystemInfo obj,
                                          object name)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            return Path.Combine(obj.FullName, name.ToString());
        }
    }
}