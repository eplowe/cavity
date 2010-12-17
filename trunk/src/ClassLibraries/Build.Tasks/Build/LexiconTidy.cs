namespace Cavity.Build
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Data;
    using Cavity.Properties;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class LexiconTidy : Task
    {
        [Required]
        public ITaskItem[] Paths { get; set; }

        public override bool Execute()
        {
            return Execute(Paths);
        }

        private bool Execute(IEnumerable<ITaskItem> paths)
        {
            if (null == paths)
            {
                Log.LogError(Resources.LexiconTidy_PathsNull_Message);
                return false;
            }

            if (0 == paths.Count())
            {
                Log.LogWarning(Resources.LexiconTidy_PathsEmpty_Message);
                return false;
            }

            return paths.All(Execute);
        }

        private bool Execute(ITaskItem path)
        {
            return Execute(new FileInfo(path.ItemSpec));
        }

        private bool Execute(FileInfo file)
        {
            file.Refresh();
            if (file.Exists)
            {
                var lexicon = new CsvLexiconStorage(file).Load();
                file.Delete();
                lexicon.Save();
                Log.LogMessage(file.FullName);
                return true;
            }

            Log.LogWarning(file.FullName);
            return false;
        }
    }
}