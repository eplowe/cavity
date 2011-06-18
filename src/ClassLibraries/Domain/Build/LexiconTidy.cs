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
            if (0 == IEnumerableExtensionMethods.Count(paths))
#else
            if (0 == paths.Count())
#endif
            {
                Log.LogWarning(Resources.LexiconTidy_PathsEmpty_Message);
                return false;
            }

            var result = true;
#if NET20
            foreach (var path in paths)
#else
            foreach (var path in paths.Where(path => !Execute(path)))
#endif
            {
#if NET20
                if (Execute(path))
                {
                    continue;
                }
#endif

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
    }
}