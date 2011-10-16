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
            return StringExtensionMethods.FormatWith(Resources.Excel_ConnectionString, file.FullName);
#else
            return Resources.Excel_ConnectionString.FormatWith(file.FullName);
#endif
        }
    }
}