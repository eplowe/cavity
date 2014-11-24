namespace Cavity.Models
{
    using System;
    using System.IO;
    using System.Threading;
    using Cavity.Threading;

    public sealed class DummyIProcessFile : ThreadedObject,
                                            IProcessFile
    {
        public void Process(FileInfo file,
                            dynamic data)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            Thread.Sleep(new TimeSpan(0, 0, 0, 0, 200));
            file.Refresh();
            file.Delete();
        }

        protected override void OnDispose()
        {
        }
    }
}