namespace Cavity.Threading
{
    using System;
    using System.Threading;

    public interface IThreadedObject : IDisposable
    {
        CancellationToken CancellationToken { get; set; }
    }
}