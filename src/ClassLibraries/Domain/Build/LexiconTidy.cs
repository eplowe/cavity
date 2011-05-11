namespace Cavity.Build
{
#if !NET20
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cavity.Collections.Generic;
    using Cavity.Data;
#endif
    using Cavity.Properties;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public sealed class LexiconTidy : Task
    {
        [Required]
        public ITaskItem[] Paths { get; set; }

        public override bool Execute()
        {
#if NET20
            Log.LogError(Resources.Unsupported_Framework_Version);
            return false;
#else
            return Execute(Paths);
#endif
        }

#if !NET20
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

            var result = true;
            foreach (var path in paths.Where(path => !Execute(path)))
            {
                if (null == path)
                {
                    continue;
                }

                if (BuildEngine.ContinueOnError)
                {
                    result = false;
                    continue;
                }

                return false;
            }

            return result;
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
                var lexicon = new CsvLexiconStorage(file).Load(NormalizationComparer.OrdinalIgnoreCase);
                file.Delete();
                lexicon.Save();
                Log.LogMessage(file.FullName);
                return true;
            }

            Log.LogWarning(file.FullName);
            return false;
        }
#endif
    }
}