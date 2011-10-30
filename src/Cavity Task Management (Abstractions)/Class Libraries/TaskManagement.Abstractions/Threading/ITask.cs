namespace Cavity.Threading
{
    using System.ComponentModel.Composition;
    using System.Threading;
    using System.Threading.Tasks;

    [InheritedExport]
    public interface ITask
    {
        TaskContinuationOptions ContinuationOptions { get; }

        TaskCreationOptions CreationOptions { get; }

        void Run(CancellationToken token);
    }
}