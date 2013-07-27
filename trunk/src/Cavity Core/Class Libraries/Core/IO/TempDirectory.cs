namespace Cavity.IO
{
    using System;
    using System.IO;
#if !NET20 && !NET35
    using System.Threading.Tasks;
#endif

    public class TempDirectory : IDisposable
    {
        public TempDirectory()
            : this(new DirectoryInfo(Path.GetTempPath()))
        {
        }

        public TempDirectory(DirectoryInfo directory)
        {
            if (null == directory)
            {
                throw new ArgumentNullException("directory");
            }

#if NET20 || NET35
            Info = new DirectoryInfo(Path.Combine(directory.FullName, Guid.NewGuid().ToString()));
#else
            Info = directory.CombineAsDirectory(Guid.NewGuid());
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
#if NET20
                if (ObjectExtensionMethods.IsNotNull(Info))
#else
                if (Info.IsNotNull())
#endif
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
#if NET20 || NET35
            foreach (var subdirectory in directory.GetDirectories())
            {
                DeleteSubdirectories(subdirectory);
                subdirectory.Delete();
            }
#else
            Parallel.ForEach(directory.EnumerateDirectories(), 
                             subdirectory =>
                                 {
                                     DeleteSubdirectories(subdirectory);
                                     subdirectory.Delete();
                                 });
#endif
        }

        private void DeleteFiles()
        {
#if NET20 || NET35
            foreach (var file in Info.GetFiles("*", SearchOption.AllDirectories))
            {
                file.Delete();
            }
#else
            Parallel.ForEach(Info.EnumerateFiles("*", SearchOption.AllDirectories), 
                             file => file.Delete());
#endif
        }
    }
}