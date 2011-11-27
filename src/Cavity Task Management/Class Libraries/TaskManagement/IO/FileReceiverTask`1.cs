namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using Cavity.Dynamic;
    using Cavity.Models;
    using Cavity.Threading;

    public abstract class FileReceiverTask<T> : StandardTask
        where T : IProcessFile, new()
    {
        protected FileReceiverTask()
            : this(null)
        {
        }

        protected FileReceiverTask(DirectoryInfo folder)
            : this(folder, "*.*", SearchOption.AllDirectories)
        {
        }

        protected FileReceiverTask(DirectoryInfo folder, string searchPattern, SearchOption searchOption)
        {
            Data = new DynamicData();
            Folder = folder;
            SearchOption = searchOption;
            SearchPattern = searchPattern;
        }

        public dynamic Data { get; protected set; }

        public DirectoryInfo Folder { get; protected set; }

        public SearchOption SearchOption { get; protected set; }

        public string SearchPattern { get; protected set; }

        public override void Run()
        {
            if (null == Folder)
            {
                throw new InvalidOperationException("The folder has not been set.");
            }

            Process(Folder
                .GetFiles(SearchPattern, SearchOption)
                .FirstOrDefault());
        }

        protected override void OnDispose()
        {
        }

        private void Process(FileInfo file)
        {
            if (null == file)
            {
                return;
            }

            Activator.CreateInstance<T>().Process(file, Data);
        }
    }
}