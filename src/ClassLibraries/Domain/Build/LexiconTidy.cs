namespace Cavity.Build
{
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
#if NET20
    using Cavity.Collections;
#endif
    using Cavity.Collections.Generic;
    using Cavity.Data;
    using Cavity.Properties;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    public class LexiconTidy : Task
    {
        [Required]
        public ITaskItem[] Paths { get; set; }

        public override bool Execute()
        {
            return Execute(Paths);
        }

        protected virtual bool Execute(FileInfo file)
        {
            if (null != file)
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
            }

            return false;
        }

        private bool Execute(IEnumerable<ITaskItem> paths)
        {
            if (null == paths)
            {
                Log.LogError(Resources.LexiconTidy_PathsNull_Message);
                return false;
            }

#if NET20
            var result = true;
            foreach (var path in paths)
            {
                if (Execute(path))
                {
                    continue;
                }

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
#else
            return !paths.Any(path => BuildEngine.ContinueOnError || !Execute(path));
#endif
        }

        private bool Execute(ITaskItem path)
        {
            return Execute(new FileInfo(path.ItemSpec));
        }
    }
}