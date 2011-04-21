namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public sealed class StandardProcess : IProcess
    {
        public StandardProcess()
        {
            Instance = new Process();
        }

        ~StandardProcess()
        {
            Dispose(false);
        }

        public int ExitCode
        {
            get
            {
                return Instance.ExitCode;
            }
        }

        public StreamReader StandardError
        {
            get
            {
                return Instance.StandardError;
            }
        }

        public StreamReader StandardOutput
        {
            get
            {
                return Instance.StandardOutput;
            }
        }

        public ProcessStartInfo StartInfo
        {
            get
            {
                return Instance.StartInfo;
            }

            set
            {
                Instance.StartInfo = value;
            }
        }

        private bool Disposed { get; set; }

        private Process Instance { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Start()
        {
            return Instance.Start();
        }

        public bool WaitForExit(int milliseconds)
        {
            return Instance.WaitForExit(milliseconds);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing && null != Instance)
                {
                    Instance.Dispose();
                    Instance = null;
                }
            }

            Disposed = true;
        }
    }
}