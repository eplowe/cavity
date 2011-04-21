namespace Cavity.Build
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Build.Sdk;
    using Cavity.IO;
    using Cavity.Properties;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class MessageLibrary : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        [Required]
        public string FrameworkSdkBin { get; set; }

        [Required]
        public string Output { get; set; }

        [Required]
        public string VCInstallDirectory { get; set; }

        [Required]
        public string WorkingDirectory { get; set; }

        public override bool Execute()
        {
            if (string.IsNullOrEmpty(WorkingDirectory))
            {
                using (var temp = new TempDirectory())
                {
                    return Execute(temp.Info);
                }
            }

            return Execute(new DirectoryInfo(WorkingDirectory));
        }

        private static IEnumerable<FileInfo> ToFileInfo(IEnumerable<ITaskItem> files)
        {
#if NET20
            foreach (var file in files)
            {
                yield return new FileInfo(file.ItemSpec);
            }
#else
            return files.Select(file => new FileInfo(file.ItemSpec));
#endif
        }

        private void CompileMessages(DirectoryInfo workingDirectory)
        {
            if (!workingDirectory.Exists)
            {
                workingDirectory.Create();
            }

            foreach (var file in ToFileInfo(Files))
            {
                var source = file.FullName;
                var destination = new FileInfo(Path.Combine(workingDirectory.FullName, file.Name)).FullName;

                File.Copy(source, destination);
            }

            var path = new FileInfo(Path.Combine(FrameworkSdkBin, "MC.exe"));
            Log.LogMessage(MessageImportance.Low, path.FullName);
            Log.LogMessage(
                MessageImportance.Normal,
                new MessageCompiler(path).Compile(workingDirectory, GetFiles(workingDirectory, "*.mc")));
        }

        private void CompileResources(DirectoryInfo workingDirectory)
        {
            var path = new FileInfo(Path.Combine(FrameworkSdkBin, "RC.exe"));
            Log.LogMessage(MessageImportance.Low, path.FullName);
            Log.LogMessage(
                MessageImportance.Normal,
                new ResourceCompiler(path).Compile(workingDirectory, GetFiles(workingDirectory, "*.rc")));
        }

        private bool Execute(DirectoryInfo workingDirectory)
        {
            try
            {
                CompileMessages(workingDirectory);
                CompileResources(workingDirectory);
                LinkResources(workingDirectory);

                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.MessageLibrary_Compiled,
                    Output);
                Log.LogMessage(MessageImportance.Normal, message);
                return true;
            }
            catch (Win32Exception exception)
            {
                Log.LogError(exception.Message);
                return false;
            }
        }

        private IEnumerable<FileInfo> GetFiles(DirectoryInfo workingDirectory, string searchPattern)
        {
            Log.LogMessage(MessageImportance.Low, searchPattern);

            var files = workingDirectory.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                Log.LogMessage(MessageImportance.Low, file.FullName);
            }

            return files;
        }

        private void LinkResources(DirectoryInfo workingDirectory)
        {
            LinkResources(workingDirectory, new FileInfo(Output));
        }

        private void LinkResources(DirectoryInfo workingDirectory,
                                   FileInfo output)
        {
            if (!output.Directory.Exists)
            {
                output.Directory.Create();
            }

            var path = new FileInfo(Path.Combine(VCInstallDirectory, "LINK.exe"));
            Log.LogMessage(MessageImportance.Low, path.FullName);
            var link = new LinkCompiler(path)
            {
                Out = new FileInfo(Path.Combine(output.Directory.FullName, output.Name))
            };

            Log.LogMessage(
                MessageImportance.Normal,
                link.Compile(workingDirectory, GetFiles(workingDirectory, "*.res"), 9000));

            link.Out.CopyTo(output.FullName);
        }
    }
}