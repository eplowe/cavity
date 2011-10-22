namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Properties;

    public static class Excel
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "The path must be to a file.")]
        public static string ConnectionString(FileInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }
            
#if NET20
            return StringExtensionMethods.FormatWith(Settings.Default.Excel, file.FullName);
#else
            return Settings.Default.Excel.FormatWith(file.FullName);
#endif
        }
    }
}