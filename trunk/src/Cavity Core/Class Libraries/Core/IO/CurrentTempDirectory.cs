namespace Cavity.IO
{
    using System;
    using System.IO;

    public class CurrentTempDirectory : TempDirectory
    {
        public CurrentTempDirectory()
            : base(Location)
        {
        }

        public static DirectoryInfo Location
        {
            get
            {
                var current = new DirectoryInfo(Environment.CurrentDirectory);
                var root = new DirectoryInfo(current.Root.FullName);
#if NET20
                return DirectoryInfoExtensionMethods.ToDirectory(root, "Temp", true);
#else
                return root.ToDirectory("Temp", true);
#endif
            }
        }
    }
}