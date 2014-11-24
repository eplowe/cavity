namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;

#endif

    public static class FileSystemInfoExtensionMethods
    {
#if !NET20 && !NET35
        public static string Combine(this FileSystemInfo obj,
                                     params object[] paths)
        {
            return Path.Combine(ToStringArray(obj, paths));
        }

        public static DirectoryInfo CombineAsDirectory(this FileSystemInfo obj,
                                                       params object[] paths)
        {
            return new DirectoryInfo(Combine(obj, paths));
        }

        public static FileInfo CombineAsFile(this FileSystemInfo obj,
                                             params object[] paths)
        {
            return new FileInfo(Combine(obj, paths));
        }
#endif

#if NET20
        public static bool NotFound(FileSystemInfo obj)
#else
        public static bool NotFound(this FileSystemInfo obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return !obj.Exists;
        }

#if !NET20 && !NET35
        private static string[] ToStringArray(FileSystemInfo obj,
                                              IEnumerable<object> paths)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var args = new List<string>
                           {
                               obj.FullName
                           };
#if NET20
            if (ObjectExtensionMethods.IsNotNull(paths))
#else
            if (paths.IsNotNull())
#endif
            {
                var range = paths
#if NET20
                    .Where(x => ObjectExtensionMethods.IsNotNull(x))
#else
                    .Where(x => x.IsNotNull())
#endif
                    .Select(path => path.ToString().RemoveIllegalFileCharacters());
                args.AddRange(range);
            }

            return args.ToArray();
        }
#endif
    }
}