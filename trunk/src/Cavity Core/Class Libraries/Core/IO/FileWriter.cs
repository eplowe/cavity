namespace Cavity.IO
{
    using System;
    using System.IO;

    public class FileWriter : StreamWriter
    {
        public FileWriter(FileInfo file)
            : base(FromFile(file))
        {
        }

        private static Stream FromFile(FileInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            return file.Open(FileMode.Append, FileAccess.Write, FileShare.Read);
        }
    }
}