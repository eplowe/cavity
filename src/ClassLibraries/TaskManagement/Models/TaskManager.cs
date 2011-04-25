namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Xml;
    using Cavity.Configuration;
    using Cavity.Diagnostics;

    public sealed class TaskManager : IManageTasks, IDisposable
    {
        ~TaskManager()
        {
            LoggingSignature.Debug();
            Dispose(false);
        }

        private static long Period
        {
            get
            {
                var milliseconds = Config.ExeSection<TaskManagementSettings>().RefreshRate.TotalMilliseconds;
                LoggingSignature.Debug(XmlConvert.ToString(milliseconds));

                return Convert.ToInt64(milliseconds);
            }
        }

        private bool Disposed { get; set; }

        private Timer Timer { get; set; }

        public void Dispose()
        {
            LoggingSignature.Debug();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IManageTasks.Continue()
        {
            LoggingSignature.Debug();
            Timer.Change(0, Period);
        }

        void IManageTasks.Pause()
        {
            LoggingSignature.Debug();
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        void IManageTasks.Shutdown()
        {
            LoggingSignature.Debug();
            Stop();
        }

        void IManageTasks.Start(IEnumerable<string> args)
        {
            LoggingSignature.Debug();
            Timer = new Timer(TimerCallback, null, 0, Period);
        }

        void IManageTasks.Stop()
        {
            LoggingSignature.Debug();
            Stop();
        }

        private static void TimerCallback(object state)
        {
            LoggingSignature.Debug();
        }

        private void Dispose(bool disposing)
        {
            LoggingSignature.Debug();
            if (!Disposed)
            {
                if (disposing && null != Timer)
                {
                    Timer.Dispose();
                    Timer = null;
                }
            }

            Disposed = true;
        }

        private void Stop()
        {
            LoggingSignature.Debug();
            if (null == Timer)
            {
                return;
            }

            Timer.Dispose();
            Timer = null;
        }
    }
}