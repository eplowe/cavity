namespace Cavity.Build
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Build.Sdk;
    using Cavity.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class MessageLibrary : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        [Required]
        public string Output { get; set; }

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

        private static IEnumerable<FileInfo> ToFiles(IEnumerable<ITaskItem> files)
        {
            return files.Select(file => new FileInfo(file.ItemSpec));
        }

        private void CompileMessages(DirectoryInfo workingDirectory)
        {
            if (!workingDirectory.Exists)
            {
                workingDirectory.Create();
            }

            foreach (var file in ToFiles(Files))
            {
                var source = file.FullName;
                var destination = new FileInfo(Path.Combine(workingDirectory.FullName, file.Name)).FullName;

                File.Copy(source, destination);
            }

            Log.LogMessage(
                MessageImportance.Normal,
                MessageCompiler.Current.Compile(workingDirectory, workingDirectory.GetFiles("*.mc", SearchOption.TopDirectoryOnly)));
        }

        private void CompileResources(DirectoryInfo workingDirectory)
        {
            Log.LogMessage(
                MessageImportance.Normal,
                ResourceCompiler.Current.Compile(workingDirectory, workingDirectory.GetFiles("*.rc", SearchOption.TopDirectoryOnly)));
        }

        private bool Execute(DirectoryInfo workingDirectory)
        {
            try
            {
                CompileMessages(workingDirectory);
                CompileResources(workingDirectory);
                LinkResources(workingDirectory);

                Log.LogMessage(MessageImportance.Normal, "Compiled {0}".FormatWith(Output));
                return true;
            }
            catch (InvalidOperationException exception)
            {
                Log.LogError(exception.Message);
                return false;
            }
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

            var link = LinkCompiler.Current;
            link.Out = output;

            Log.LogMessage(
                MessageImportance.Normal,
                link.Compile(workingDirectory, workingDirectory.GetFiles("*.res", SearchOption.TopDirectoryOnly)));
        }
    }
}