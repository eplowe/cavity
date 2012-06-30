namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public sealed class Name
    {
        private Name()
        {
            Files = new List<FileInfo>();
        }

        public IList<FileInfo> Files { get; private set; }

        public static Name Load(string name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            name = name.Trim();
            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            return Search(name) ?? Load(new FileInfo(name));
        }

        private static Name Load(FileInfo file)
        {
            if (!file.Exists)
            {
                throw new FileNotFoundException(file.FullName);
            }

            var result = new Name();

            result.Files.Add(file);

            return result;
        }

        private static Name Search(string name)
        {
            if (!name.Contains("*"))
            {
                return null;
            }

            DirectoryInfo directory;
            string pattern;
            var option = SearchOption.TopDirectoryOnly;
            if (name.Contains(@"\**\"))
            {
                var index = name.IndexOf(@"\**\", StringComparison.Ordinal);
                directory = new DirectoryInfo(name.Substring(0, index));
                pattern = name.Substring(index + 4);
                option = SearchOption.AllDirectories;
            }
            else
            {
                var index = name.IndexOf('*');
                directory = new DirectoryInfo(name.Substring(0, index));
                pattern = name.Substring(index);
            }

            var result = new Name();

            foreach (var file in directory.GetFiles(pattern, option))
            {
                result.Files.Add(file);
            }

            return result;
        }
    }
}