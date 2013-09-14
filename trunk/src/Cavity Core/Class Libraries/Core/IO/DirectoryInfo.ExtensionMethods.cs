namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
#if !NET20 && !NET35
    using System.Threading.Tasks;
#endif

    public static class DirectoryInfoExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static void CopyTo(DirectoryInfo source, 
                                  DirectoryInfo destination, 
                                  bool replace)
#else
        public static void CopyTo(this DirectoryInfo source,
                                  DirectoryInfo destination,
                                  bool replace)
#endif
        {
            CopyTo(source, destination, replace, "*.*");
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static void CopyTo(DirectoryInfo source, 
                                  DirectoryInfo destination, 
                                  bool replace, 
                                  string pattern)
#else
        public static void CopyTo(this DirectoryInfo source,
                                  DirectoryInfo destination,
                                  bool replace,
                                  string pattern)
#endif
        {
            if (null == source)
            {
                throw new ArgumentNullException("source");
            }

            if (null == destination)
            {
                throw new ArgumentNullException("destination");
            }

            if (null == pattern)
            {
                throw new ArgumentNullException("pattern");
            }

            if (0 == pattern.Length)
            {
                throw new ArgumentOutOfRangeException("pattern");
            }

#if NET20 || NET35
            foreach (var file in source.GetFiles(pattern, SearchOption.AllDirectories))
#else
            Parallel.ForEach(source.EnumerateFiles(pattern, SearchOption.AllDirectories),
                             file =>
#endif
                                 {
                                     var target = new FileInfo(file.FullName.Replace(source.FullName, destination.FullName));
                                     if (target.Exists)
                                     {
                                         if (replace)
                                         {
                                             target.Delete();
                                         }
                                         else
                                         {
#if NET20 || NET35
                                             continue;
#else
                                             return;
#endif
                                         }
                                     }
#if NET20 || NET35
                                     Make(target.Directory);
                                     FileInfoExtensionMethods.CopyTo(file, target);
                                 }
#else
                                     target.Directory.Make();
                                     file.CopyTo(target);
                                 });
#endif
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> CsvFiles(DirectoryInfo directory)
#else
        public static IEnumerable<FileInfo> CsvFiles(this DirectoryInfo directory)
#endif
        {
            return EnumerateFiles(directory, "*.csv");
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> CsvFiles(DirectoryInfo directory, 
                                                     SearchOption searchOption)
#else
        public static IEnumerable<FileInfo> CsvFiles(this DirectoryInfo directory,
                                                     SearchOption searchOption)
#endif
        {
            return EnumerateFiles(directory, "*.csv", searchOption);
        }

#if NET20 || NET35
#else
        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo obj,
                                                                      Func<DirectoryInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateDirectories().Where(predicate);
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo obj,
                                                                      SearchOption searchOption,
                                                                      Func<DirectoryInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateDirectories("*", searchOption).Where(predicate);
        }

        public static IEnumerable<DirectoryInfo> EnumerateDirectories(this DirectoryInfo obj,
                                                                      string searchPattern,
                                                                      SearchOption searchOption,
                                                                      Func<DirectoryInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateDirectories(searchPattern, searchOption).Where(predicate);
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo obj,
                                                           Func<FileInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateFiles().Where(predicate);
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo obj,
                                                           SearchOption searchOption,
                                                           Func<FileInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateFiles("*", searchOption).Where(predicate);
        }

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo obj,
                                                           string searchPattern,
                                                           SearchOption searchOption,
                                                           Func<FileInfo, bool> predicate)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.EnumerateFiles(searchPattern, searchOption).Where(predicate);
        }
#endif

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static int LineCount(DirectoryInfo directory, 
                                    string searchPattern, 
                                    SearchOption searchOption)
#else
        public static int LineCount(this DirectoryInfo directory,
                                    string searchPattern,
                                    SearchOption searchOption)
#endif
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

#if NET20
            if (FileSystemInfoExtensionMethods.NotFound(directory))
#else
            if (directory.NotFound())
#endif
            {
                throw new DirectoryNotFoundException(directory.FullName);
            }

#if NET20
            var result = 0;
            foreach (var file in directory.GetFiles(searchPattern, searchOption))
            {
                result += FileInfoExtensionMethods.LineCount(file);
            }

            return result;
#elif NET35
            return directory
                .GetFiles(searchPattern, searchOption)
                .Sum(file => file.LineCount());
#else
            return directory
                .EnumerateFiles(searchPattern, searchOption)
                .Sum(file => file.LineCount());
#endif
        }

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
#if NET20
            if (FileSystemInfoExtensionMethods.NotFound(obj))
#else
            if (obj.NotFound())
#endif
            {
                obj.Create();
                obj.Refresh();
            }

            return obj;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static void MoveTo(DirectoryInfo source, 
                                  DirectoryInfo destination, 
                                  bool replace)
#else
        public static void MoveTo(this DirectoryInfo source,
                                  DirectoryInfo destination,
                                  bool replace)
#endif
        {
            MoveTo(source, destination, replace, "*.*");
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static void MoveTo(DirectoryInfo source, 
                                  DirectoryInfo destination, 
                                  bool replace, 
                                  string pattern)
#else
        public static void MoveTo(this DirectoryInfo source,
                                  DirectoryInfo destination,
                                  bool replace,
                                  string pattern)
#endif
        {
            if (null == source)
            {
                throw new ArgumentNullException("source");
            }

            if (null == destination)
            {
                throw new ArgumentNullException("destination");
            }

            if (null == pattern)
            {
                throw new ArgumentNullException("pattern");
            }

            if (0 == pattern.Length)
            {
                throw new ArgumentOutOfRangeException("pattern");
            }

#if NET20 || NET35
            foreach (var file in source.GetFiles(pattern, SearchOption.AllDirectories))
#else
            Parallel.ForEach(source.EnumerateFiles(pattern, SearchOption.AllDirectories),
                             file =>
#endif
                                 {
                                     var target = new FileInfo(file.FullName.Replace(source.FullName, destination.FullName));
                                     if (target.Exists)
                                     {
                                         if (replace)
                                         {
                                             target.Delete();
                                         }
                                         else
                                         {
#if NET20 || NET35
                                             continue;
#else
                                             return;
#endif
                                         }
                                     }
#if NET20 || NET35
                                     Make(target.Directory);
                                     FileInfoExtensionMethods.MoveTo(file, target);
                                 }
#else
                                     target.Directory.Make();
                                     file.MoveTo(target);
                                 });
#endif
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want strong typing here.")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Code stack has been reviewed.")]
#if NET20
        public static void RobocopyTo(DirectoryInfo source,
                                      DirectoryInfo destination)
#else
        public static void RobocopyTo(this DirectoryInfo source,
                                      DirectoryInfo destination)
#endif
        {
            RobocopyTo(source, destination, false);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want strong typing here.")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Code stack has been reviewed.")]
#if NET20
        public static void RobocopyTo(DirectoryInfo source,
                                      DirectoryInfo destination,
                                      bool move)
#else
        public static void RobocopyTo(this DirectoryInfo source,
                                      DirectoryInfo destination,
                                      bool move)
#endif
        {
            if (null == source)
            {
                throw new ArgumentNullException("source");
            }

            if (!source.Exists)
            {
                throw new DirectoryNotFoundException(source.FullName);
            }

            if (null == destination)
            {
                throw new ArgumentNullException("destination");
            }

            if (!destination.Exists)
            {
                Make(destination);
            }

            Process.Start(new ProcessStartInfo("ROBOCOPY")
            {
                Arguments = string.Format(CultureInfo.InvariantCulture, "\"{0}\" \"{1}\" /S{2}", source.FullName, destination.FullName, move ? " /MOVE" : string.Empty),
            }).WaitForExit();
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want strong typing here.")]
#if NET20
        public static void SetDate(DirectoryInfo directory,
                                   DateTime date)
#else
        public static void SetDate(this DirectoryInfo directory,
                                   DateTime date)
#endif
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

            directory.Refresh();
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException(directory.FullName);
            }

            Directory.SetCreationTimeUtc(directory.FullName, date);
            Directory.SetLastAccessTimeUtc(directory.FullName, date);
            Directory.SetLastWriteTimeUtc(directory.FullName, date);

            directory.Refresh();
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> TextFiles(DirectoryInfo directory)
#else
        public static IEnumerable<FileInfo> TextFiles(this DirectoryInfo directory)
#endif
        {
            return EnumerateFiles(directory, "*.txt");
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> TextFiles(DirectoryInfo directory, 
                                                      SearchOption searchOption)
#else
        public static IEnumerable<FileInfo> TextFiles(this DirectoryInfo directory,
                                                      SearchOption searchOption)
#endif
        {
            return EnumerateFiles(directory, "*.txt", searchOption);
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
                                                IEnumerable<object> names)
#else
        public static DirectoryInfo ToDirectory(this DirectoryInfo obj,
                                                IEnumerable<object> names)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == names)
            {
                throw new ArgumentNullException("names");
            }

            var result = new DirectoryInfo(obj.FullName);
#if NET20
            foreach (var name in names)
            {
                result = ToDirectory(result, name, true);
            }

            return result;
#else
            return names.Aggregate(result, (current,
                                            name) => current.ToDirectory(name, true));
#endif
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
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

#if NET20 || NET35
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var dir = new DirectoryInfo(Path.Combine(obj.FullName, StringExtensionMethods.RemoveIllegalFileCharacters(name.ToString())));
#else
            var dir = obj.CombineAsDirectory(name);
#endif
            if (create)
            {
                Make(dir);
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
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

#if NET20 || NET35
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return new FileInfo(Path.Combine(obj.FullName, StringExtensionMethods.RemoveIllegalFileCharacters(name.ToString())));
#else
            return obj.CombineAsFile(name);
#endif
        }

#if NET20
        public static DirectoryInfo ToParent(DirectoryInfo obj)
#else
        public static DirectoryInfo ToParent(this DirectoryInfo obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.Parent;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> XmlFiles(DirectoryInfo directory)
#else
        public static IEnumerable<FileInfo> XmlFiles(this DirectoryInfo directory)
#endif
        {
            return EnumerateFiles(directory, "*.xml");
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
#if NET20
        public static IEnumerable<FileInfo> XmlFiles(DirectoryInfo directory, 
                                                      SearchOption searchOption)
#else
        public static IEnumerable<FileInfo> XmlFiles(this DirectoryInfo directory,
                                                      SearchOption searchOption)
#endif
        {
            return EnumerateFiles(directory, "*.xml", searchOption);
        }

#if NET20
        private static IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo directory,
                                                            string pattern)
        {
            return EnumerateFiles(directory, pattern, SearchOption.TopDirectoryOnly);
        }

        private static IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo directory,
                                                            string pattern,
                                                            SearchOption searchOption)
#else
        private static IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo directory,
                                                            string pattern,
                                                            SearchOption searchOption = SearchOption.TopDirectoryOnly)
#endif
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

#if NET20
            if (FileSystemInfoExtensionMethods.NotFound(directory))
#else
            if (directory.NotFound())
#endif
            {
                throw new DirectoryNotFoundException(directory.FullName);
            }

#if NET20 || NET35
            foreach (var file in directory.GetFiles(pattern, searchOption))
            {
                yield return file;
            }
#else
            return directory.EnumerateFiles(pattern, searchOption);
#endif
        }
    }
}