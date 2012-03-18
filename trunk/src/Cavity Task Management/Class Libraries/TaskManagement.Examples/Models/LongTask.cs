namespace Cavity.Models
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    using Cavity.Threading;

    public sealed class LongTask : StandardTask
    {
        public override void Run()
        {
            Trace.TraceInformation("long running task started {0}".FormatWith(DateTime.UtcNow.ToXmlString()));
            Thread.Sleep(new TimeSpan(0, 0, 30));
            Trace.TraceInformation("long running task finished {0}".FormatWith(DateTime.UtcNow.ToXmlString()));
        }

        protected override void OnDispose()
        {
        }
    }
}