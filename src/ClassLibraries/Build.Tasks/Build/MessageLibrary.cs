namespace Cavity.Build
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Build.Sdk;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class MessageLibrary : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        [Required]
        public string WorkingDirectory { get; set; }

        public override bool Execute()
        {
            try
            {
                var dir = new DirectoryInfo(WorkingDirectory);
                var output = MessageCompiler.Current.Compile(dir, ToFiles(Files));
                Log.LogMessage(MessageImportance.Normal, output);

                return true;
            }
            catch (InvalidOperationException exception)
            {
                Log.LogError(exception.Message);
                return false;
            }
        }

        private static IEnumerable<FileInfo> ToFiles(IEnumerable<ITaskItem> files)
        {
            return files.Select(file => new FileInfo(file.ItemSpec));
        }
    }
}