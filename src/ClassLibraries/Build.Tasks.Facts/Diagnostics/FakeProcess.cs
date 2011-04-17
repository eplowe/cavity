namespace Cavity.Diagnostics
{
    using System.Diagnostics;
    using System.IO;

    public sealed class FakeProcess : IProcess
    {
        public int ExitCode
        {
            get
            {
                return 0;
            }
        }

        public StreamReader StandardError
        {
            get
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                var reader = new StreamReader(stream);
                writer.Write("error");
                writer.Flush();
                stream.Position = 0;

                return reader;
            }
        }

        public StreamReader StandardOutput
        {
            get
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                var reader = new StreamReader(stream);
                writer.Write("fake");
                writer.Flush();
                stream.Position = 0;

                return reader;
            }
        }

        public ProcessStartInfo StartInfo { get; set; }

        public void Dispose()
        {
        }

        public bool Start()
        {
            return true;
        }
    }
}