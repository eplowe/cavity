namespace Cavity.IO
{
    using System;
    using System.IO;

    public sealed class TempDirectory : IDisposable
    {
        public TempDirectory()
        {

            Info = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            Info.Create();
        }

        ~TempDirectory()
        {
            Dispose(false);
        }

        public DirectoryInfo Info { get; private set; }

        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void DeleteSubdirectories(DirectoryInfo directory)
        {
            foreach (var subdirectory in directory.GetDirectories())
            {
                DeleteSubdirectories(subdirectory);
                subdirectory.Delete();
            }
        }

        private void DeleteFiles()
        {
            foreach (var file in Info.GetFiles("*", SearchOption.AllDirectories))
            {
                file.Delete();
            }
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
                        DeleteFiles();
                        DeleteSubdirectories(Info);
                        Info.Delete();
                    }

                    Info = null;
                }
            }

            Disposed = true;
        }
    }
}