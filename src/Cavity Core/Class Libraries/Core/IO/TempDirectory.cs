namespace Cavity.IO
{
    using System;
    using System.IO;
#if NET40
    using System.Threading.Tasks;
#endif

    public class TempDirectory : IDisposable
    {
        public TempDirectory()
            : this(new DirectoryInfo(Path.GetTempPath()))
        {
        }

        public TempDirectory(DirectoryInfo dir)
        {
            if (null == dir)
            {
                throw new ArgumentNullException("dir");
            }
            
#if NET40
            Info = dir.CombineAsDirectory(Guid.NewGuid());
#else
            Info = new DirectoryInfo(Path.Combine(dir.FullName, Guid.NewGuid().ToString()));
#endif
            Info.Create();
        }

        ~TempDirectory()
        {
            Dispose(false);
        }

        public DirectoryInfo Info { get; protected set; }

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
                        DeleteFiles();
                        DeleteSubdirectories(Info);
                        Info.Delete();
                    }

                    Info = null;
                }
            }

            Disposed = true;
        }

        private static void DeleteSubdirectories(DirectoryInfo directory)
        {
#if NET40
            Parallel.ForEach(directory.EnumerateDirectories(), subdirectory =>
            {
                DeleteSubdirectories(subdirectory);
                subdirectory.Delete();
            });
#else
            foreach (var subdirectory in directory.GetDirectories())
            {
                DeleteSubdirectories(subdirectory);
                subdirectory.Delete();
            }
#endif
        }

        private void DeleteFiles()
        {
#if NET40
            Parallel.ForEach(Info.EnumerateFiles("*", SearchOption.AllDirectories),
                             file => file.Delete());
#else
            foreach (var file in Info.GetFiles("*", SearchOption.AllDirectories))
            {
                file.Delete();
            }
#endif
        }
    }
}