namespace Cavity.Models
{
    using System.IO;
    using Cavity.Threading;

    public abstract class FileProcessor : ThreadedObject, IProcessFile
    {
        public abstract void Process(FileInfo file,
                                     dynamic data);
    }
}