namespace Cavity.Threading
{
    using System;
    using System.ComponentModel.Composition;
    using System.Threading;
    using System.Threading.Tasks;

    [InheritedExport]
    public interface ITask : IDisposable
    {
        CancellationToken CancellationToken { get; }

        TaskContinuationOptions ContinuationOptions { get; }

        TaskCreationOptions CreationOptions { get; }

        void Run(CancellationToken token);
    }
}