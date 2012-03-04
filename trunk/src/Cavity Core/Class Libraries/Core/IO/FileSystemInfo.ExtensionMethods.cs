namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class FileSystemInfoExtensionMethods
    {
        public static string Combine(this FileSystemInfo obj, params object[] paths)
        {
            return Path.Combine(ToStringArray(obj, paths));
        }

        public static DirectoryInfo CombineAsDirectory(this FileSystemInfo obj, params object[] paths)
        {
            return new DirectoryInfo(Combine(obj, paths));
        }

        public static FileInfo CombineAsFile(this FileSystemInfo obj, params object[] paths)
        {
            return new FileInfo(Combine(obj, paths));
        }

        private static string[] ToStringArray(FileSystemInfo obj, IEnumerable<object> paths)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var args = new List<string>
            {
                obj.FullName
            };

            if (null != paths)
            {
                args.AddRange(paths.Where(x => null != x).Select(path => path.ToString().RemoveIllegalFileCharacters()));
            }

            return args.ToArray();
        }
    }
}