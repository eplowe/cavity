namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public sealed class TaskManager : IManageTasks
    {
        private Timer Timer { get; set; }

        void IManageTasks.Continue()
        {
        }

        void IManageTasks.Pause()
        {
        }

        void IManageTasks.Shutdown()
        {
            Stop();
        }

        void IManageTasks.Start(IEnumerable<string> args)
        {
            Timer = new Timer(TimerCallback, null, 0, new TimeSpan(0, 0, 5).Milliseconds);
        }

        void IManageTasks.Stop()
        {
            Stop();
        }

        private static void TimerCallback(object state)
        {
            ////if (endProcess == true)
            ////{
            ////    Timer.Dispose();
            ////    return;
            ////}
        }

        private void Stop()
        {
            if (null == Timer)
            {
                return;
            }

            Timer.Dispose();
            Timer = null;
        }
    }
}