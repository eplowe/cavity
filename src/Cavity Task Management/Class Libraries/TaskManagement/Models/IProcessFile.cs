namespace Cavity.Models
{
    using System.IO;

    using Cavity.Threading;

    public interface IProcessFile : IThreadedObject
    {
        void Process(FileInfo file, 
                     dynamic data);
    }
}