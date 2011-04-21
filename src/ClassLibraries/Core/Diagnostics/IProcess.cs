namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public interface IProcess : IDisposable
    {
        int ExitCode { get; }

        StreamReader StandardError { get; }

        StreamReader StandardOutput { get; }

        ProcessStartInfo StartInfo { get; set; }

        bool Start();

        bool WaitForExit(int milliseconds);
    }
}