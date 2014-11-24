namespace Cavity.IO
{
    using System.IO;
    using Cavity.Models;

    public sealed class DerivedFileReceiverTask : FileReceiverTask<DummyIProcessFile>
    {
        public DerivedFileReceiverTask()
        {
        }

        public DerivedFileReceiverTask(DirectoryInfo folder)
            : base(folder)
        {
        }

        public DerivedFileReceiverTask(DirectoryInfo folder,
                                       string searchPattern,
                                       SearchOption searchOption)
            : base(folder, searchPattern, searchOption)
        {
        }
    }
}