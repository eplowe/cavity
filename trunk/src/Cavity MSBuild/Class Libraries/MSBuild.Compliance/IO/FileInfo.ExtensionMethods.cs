namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class FileInfoExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static bool FixNewLine(this FileInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException(file.FullName);
            }

            var before = file.ReadToEnd();
            var after = before
                .Replace("\n", "\r\n", StringComparison.OrdinalIgnoreCase)
                .Replace("\r\r\n", "\r\n", StringComparison.OrdinalIgnoreCase);
            if (before == after)
            {
                return false;
            }

            file.Truncate(after);
            return true;
        }
    }
}