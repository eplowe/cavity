namespace Cavity.Build.Sdk
{
    using System;
    using System.IO;
    using Cavity.IO;
    using Cavity.Win32;

    public sealed class FakePlatformSdk : IDisposable
    {
        public FakePlatformSdk()
        {
            InstallationFolder = new TempDirectory();

            RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
            RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = InstallationFolder.Info.FullName;

            InstallationFolder.Info.CreateSubdirectory("bin");
            var exe = new FileInfo(Path.Combine(InstallationFolder.Info.FullName, @"bin\mc.exe"));
            using (var stream = exe.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(string.Empty);
            }
        }

        ~FakePlatformSdk()
        {
            Dispose(false);
        }

        public TempDirectory InstallationFolder { get; private set; }

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
                if (disposing)
                {
                    RegistryFacade.Reset();
                    if (null != InstallationFolder)
                    {
                        InstallationFolder.Dispose();
                        InstallationFolder = null;
                    }
                }
            }

            Disposed = true;
        }
    }
}