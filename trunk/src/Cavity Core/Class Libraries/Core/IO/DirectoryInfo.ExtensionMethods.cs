namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
            Parallel.ForEach(source.EnumerateFiles(pattern, SearchOption.AllDirectories), file =>
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

                                                                                                  if (null != target.Directory &&
                                                                                                      !target.Directory.Exists)
                                                                                                  {
                                                                                                      target.Directory.Create();
                                                                                                  }

                                                                                                  file.CopyTo(target.FullName);
#if NET20 || NET35
            }
#else
                                                                                              });
#endif
            foreach (var file in source.GetFiles(pattern, SearchOption.AllDirectories))
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
                        continue;
                    }
                }

                if (null != target.Directory &&
                    !target.Directory.Exists)
                {
                    target.Directory.Create();
                }

                file.CopyTo(target.FullName);
            }
        }

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

            if (!directory.Exists)
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
            if (!obj.Exists)
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
            Parallel.ForEach(source.EnumerateFiles(pattern, SearchOption.AllDirectories), file =>
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

                                                                                                  if (null != target.Directory &&
                                                                                                      !target.Directory.Exists)
                                                                                                  {
                                                                                                      target.Directory.Create();
                                                                                                  }

                                                                                                  file.MoveTo(target.FullName);
#if NET20 || NET35
            }
#else
                                                                                              });
#endif
            foreach (var file in source.GetFiles(pattern, SearchOption.AllDirectories))
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
                        continue;
                    }
                }

                if (null != target.Directory &&
                    !target.Directory.Exists)
                {
                    target.Directory.Create();
                }

                file.CopyTo(target.FullName);
            }
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
    }
}