namespace Cavity.IO
{
    using System;
    using System.IO;
    using Cavity.Models;

    public static class FileExtensionMethods
    {
        public static BritishPostcode ToPostcode(this FileInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            return file.RemoveExtension().Name;
        }
    }
}