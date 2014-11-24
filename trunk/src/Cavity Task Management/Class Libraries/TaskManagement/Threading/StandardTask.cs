namespace Cavity.Threading
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Cavity.Diagnostics;

    public abstract class StandardTask : DisposableObject,
                                         ITask
    {
        public CancellationToken CancellationToken { get; private set; }

        public TaskContinuationOptions ContinuationOptions
        {
            get
            {
                return TaskContinuationOptions.None;
            }
        }

        public TaskCreationOptions CreationOptions
        {
            get
            {
                return TaskCreationOptions.None;
            }
        }

        public abstract void Run();

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Don't let exceptions leak out of tasks.")]
        public virtual void Run(CancellationToken token)
        {
            try
            {
                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                CancellationToken = token;
                CancellationToken.Register(Dispose);
                Run();
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}", exception);
            }
        }
    }
}