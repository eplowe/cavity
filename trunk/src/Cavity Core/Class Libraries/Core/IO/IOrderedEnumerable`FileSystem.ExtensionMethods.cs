#if !NET20 && !NET35
namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public static class IOrderedEnumerableOfFileSystemExtensionMethods
    {
        public static void DeleteAllExceptLast(this IOrderedEnumerable<FileInfo> files)
        {
            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

            var list = files.ToList();
            if (list.Count.In(0, 1))
            {
                return;
            }

            list.Remove(list.Last());

            Parallel.ForEach(list, file => file.Delete());
        }

        public static void DeleteAllExceptLast(this IOrderedEnumerable<DirectoryInfo> directories)
        {
            if (null == directories)
            {
                throw new ArgumentNullException("directories");
            }

            var list = directories.ToList();
            if (list.Count.In(0, 1))
            {
                return;
            }

            list.Remove(list.Last());

            Parallel.ForEach(list, directory => directory.Delete(true));
        }
    }
}
#endif