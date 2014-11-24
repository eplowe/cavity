namespace Cavity.IO
{
    using System.IO;
    using Cavity.Threading;

    public interface IReceiveFile : IThreadedObject
    {
        void Receive(string path);

        void Receive(FileInfo file);
    }
}