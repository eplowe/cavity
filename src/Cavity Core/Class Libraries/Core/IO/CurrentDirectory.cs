namespace Cavity.IO
{
    using System;
    using System.IO;

    public class CurrentDirectory
    {
        public CurrentDirectory()
            : this(Directory.GetCurrentDirectory())
        {
        }

        public CurrentDirectory(string path)
            : this(new DirectoryInfo(path))
        {
        }

        public CurrentDirectory(DirectoryInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            Info = info;
        }

        public DirectoryInfo Info { get; private set; }
    }
}