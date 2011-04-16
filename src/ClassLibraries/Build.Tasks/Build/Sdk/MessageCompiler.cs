namespace Cavity.Build.Sdk
{
    using System.IO;
    using Cavity.Win32;

    public sealed class MessageCompiler
    {
        private MessageCompiler(FileInfo location)
        {
            Location = location;
        }

        public static MessageCompiler Current
        {
            get
            {
                const string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows";
                var version = RegistryFacade.GetValue(key, "CurrentVersion");
                if (null == version)
                {
                    return null;
                }

                var installation = (string)RegistryFacade.GetValue(@"{0}\{1}".FormatWith(key, version), "InstallationFolder");
                if (null == installation)
                {
                    return null;
                }

                var bin = new DirectoryInfo(Path.Combine(installation, "Bin"));
                if (!bin.Exists)
                {
                    return null;
                }

                var exe = new FileInfo(Path.Combine(bin.FullName, "MC.exe"));
                return exe.Exists
                    ? new MessageCompiler(exe)
                    : null;
            }
        }

        public FileInfo Location { get; private set; }
    }
}