namespace Cavity.Threading
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Cavity.Diagnostics;

    public abstract class StandardTask : ThreadedObject, ITask
    {
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

        private static IThreadedObject Instance { get; set; }

        public abstract IThreadedObject CreateInstance();

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Don't let exceptions leak out of tasks.")]
        public virtual void Run(CancellationToken token)
        {
            try
            {
                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                token.Register(Dispose);
                Instance = Instance ?? SetToken(CreateInstance(), token);
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}", exception);
            }
        }

        protected override void OnDispose()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null != Instance)
            {
                Instance.Dispose();
                Instance = null;
            }
        }

        private static IThreadedObject SetToken(IThreadedObject instance,
                                                CancellationToken token)
        {
            if (null == instance)
            {
                throw new ArgumentNullException("instance");
            }

            instance.CancellationToken = token;

            return instance;
        }
    }
}