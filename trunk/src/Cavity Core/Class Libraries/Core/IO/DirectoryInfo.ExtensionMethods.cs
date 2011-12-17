namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

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

            var buffer = new StringBuilder();
            foreach (int c in name.ToString())
            {
                if (32 > c)
                {
                    // Control characters
                    continue;
                }

                switch (c)
                {
                    case 34: // "
                    case 42: // *
                    case 47: // /
                    case 58: // :
                    case 60: // <
                    case 62: // >
                    case 63: // ?
                    case 92: // \
                    case 124: // |
                    case 127: // DEL
                        break;

                    default:
                        buffer.Append((char)c);
                        break;
                }
            }

#if NET20
            var value = StringExtensionMethods.NormalizeWhiteSpace(buffer.ToString()).Trim();
#else
            var value = buffer.ToString().NormalizeWhiteSpace().Trim();
#endif
            if (0 == value.Length)
            {
#if NET20
                var message = StringExtensionMethods.FormatWith("'{0}' was empty when trimmed.", buffer.ToString());
#else
                var message = "'{0}' was empty when trimmed.".FormatWith(name.ToString());
#endif
                throw new ArgumentOutOfRangeException("name", name, message);
            }

            try
            {
                return Path.Combine(obj.FullName, value);
            }
            catch (ArgumentException exception)
            {
#if NET20
                var message = StringExtensionMethods.FormatWith("'{0}' is an invalid name.", name.ToString());
#else
                var message = "'{0}' is an invalid name.".FormatWith(name.ToString());
#endif
                throw new ArgumentOutOfRangeException(message, exception);
            }
        }
    }
}