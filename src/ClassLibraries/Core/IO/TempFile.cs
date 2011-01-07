namespace Cavity.IO
{
    using System;
    using System.IO;

    public sealed class TempFile : IDisposable
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

        public FileInfo Info { get; private set; }

        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing && null != Info)
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