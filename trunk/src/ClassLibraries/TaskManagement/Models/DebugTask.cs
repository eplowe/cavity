namespace Cavity.Models
{
    using System.ComponentModel.Composition;
    using Cavity.Data;
    using Cavity.Diagnostics;

    [Export(typeof(ITask))]
    public sealed class DebugTask : ITask
    {
        public DataCollection Execute(DataCollection configuration)
        {
            LoggingSignature.Debug();

            return configuration;
        }
    }
}