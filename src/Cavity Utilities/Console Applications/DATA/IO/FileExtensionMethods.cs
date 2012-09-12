namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class FileExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static FileInfo ChangeExtension(this FileInfo file, string extension)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            var path = file.FullName.RemoveFromEnd(file.Extension, StringComparison.OrdinalIgnoreCase);

            return new FileInfo(path.Append(extension));
        }
    }
}