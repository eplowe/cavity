namespace Cavity.IO
{
    using System;
    using System.IO;

    public class TempFile : IDisposable
    {
        public TempFile()
        {
            Info = new FileInfo(Path.GetTempFileName());
        }

        public TempFile(DirectoryInfo directory)
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

            Info = new FileInfo(Path.Combine(directory.FullName, Guid.NewGuid().ToString()));
        }

        ~TempFile()
        {
            Dispose(false);
        }

        public FileInfo Info { get; protected set; }

        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                if (null != Info)
                {
                    Info.Refresh();
                    if (Info.Exists)
                    {
                        Info.Delete();
                    }

                    Info = null;
                }
            }

            Disposed = true;
        }
    }
}