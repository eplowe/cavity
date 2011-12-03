namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class DirectoryInfoExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static DirectoryInfo Make(DirectoryInfo obj)
#else
        public static DirectoryInfo Make(this DirectoryInfo obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            obj.Refresh();
            if (!obj.Exists)
            {
                obj.Create();
                obj.Refresh();
            }

            return obj;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static DirectoryInfo ToDirectory(DirectoryInfo obj,
                                                object name)
#else
        public static DirectoryInfo ToDirectory(this DirectoryInfo obj,
                                                object name)
#endif
        {
            return ToDirectory(obj, name, false);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static DirectoryInfo ToDirectory(DirectoryInfo obj,
                                                object name,
                                                bool create)
#else
        public static DirectoryInfo ToDirectory(this DirectoryInfo obj,
                                                object name,
                                                bool create)
#endif
        {
            var dir = new DirectoryInfo(PathCombine(obj, name));
            if (create)
            {
#if NET20
                DirectoryInfoExtensionMethods.Make(dir);
#else
                dir.Make();
#endif
            }

            return dir;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static FileInfo ToFile(DirectoryInfo obj,
                                      object name)
#else
        public static FileInfo ToFile(this DirectoryInfo obj,
                                      object name)
#endif
        {
            return new FileInfo(PathCombine(obj, name));
        }

        private static string PathCombine(FileSystemInfo obj,
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

#if NET20
            var value = StringExtensionMethods.ReplaceAllWith(name.ToString(), string.Empty, StringComparison.Ordinal, "\\", "/", ":", "*", "?", "\"", "<", ">", "|");
#else
            var value = name
                .ToString()
                .ReplaceAllWith(string.Empty, StringComparison.Ordinal, "\\", "/", ":", "*", "?", "\"", "<", ">", "|");
#endif

            return Path.Combine(obj.FullName, value);
        }
    }
}