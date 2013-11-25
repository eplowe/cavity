namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Models;

    public static class DirectoryExtensionMethods
    {
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want strong typing here.")]
        public static BritishPostcode ToPostcode(this DirectoryInfo directory)
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

            return directory.Name;
        }
    }
}