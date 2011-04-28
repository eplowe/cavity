namespace Cavity.Models
{
    using System.ComponentModel.Composition;
    using Cavity.Diagnostics;

    [Export(typeof(ITask))]
    public sealed class DebugTask : ITask
    {
        public void Run()
        {
            LoggingSignature.Debug();
        }
    }
}