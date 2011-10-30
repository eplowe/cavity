namespace Cavity.Models
{
    using System.Diagnostics;
    using Cavity.Threading;

    public sealed class ShortTask : StandardTask
    {
        public override IThreadedObject CreateInstance()
        {
            Trace.TraceInformation("short running task");

            return null;
        }
    }
}