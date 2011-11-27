namespace Cavity.Models
{
    using System.Diagnostics;
    using Cavity.Threading;

    public sealed class ShortTask : StandardTask
    {
        public override void Run()
        {
            Trace.TraceInformation("short running task");
        }

        protected override void OnDispose()
        {
        }
    }
}