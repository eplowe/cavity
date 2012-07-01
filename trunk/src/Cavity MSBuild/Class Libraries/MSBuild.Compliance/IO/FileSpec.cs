namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public sealed class FileSpec : IEnumerable<FileInfo>
    {
        public FileSpec(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            value = value.Trim();
            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            Value = value;
        }

        private string Value { get; set; }

        public IEnumerator<FileInfo> GetEnumerator()
        {
            if (!Value.Contains("*"))
            {
                var file = new FileInfo(Value);
                if (!file.Exists)
                {
                    throw new FileNotFoundException(file.FullName);
                }

                yield return file;
                yield break;
            }

            DirectoryInfo directory;
            string pattern;
            var option = SearchOption.TopDirectoryOnly;
            if (Value.Contains(@"\**\"))
            {
                var index = Value.IndexOf(@"\**\", StringComparison.Ordinal);
                directory = new DirectoryInfo(Value.Substring(0, index));
                pattern = Value.Substring(index + 4);
                option = SearchOption.AllDirectories;
            }
            else
            {
                var index = Value.IndexOf('*');
                directory = new DirectoryInfo(Value.Substring(0, index));
                pattern = Value.Substring(index);
            }

            foreach (var file in directory.EnumerateFiles(pattern, option))
            {
                yield return file;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}