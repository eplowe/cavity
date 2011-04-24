namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;
    using log4net;

    public sealed class TaskManager : IManageTasks, IDisposable
    {
        [ThreadStatic]
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        ~TaskManager()
        {
            Dispose(false);
        }

        private bool Disposed { get; set; }

        private Timer Timer { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IManageTasks.Continue()
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            Timer.Change(0, new TimeSpan(0, 0, 5).Milliseconds);
        }

        void IManageTasks.Pause()
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        void IManageTasks.Shutdown()
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            Stop();
        }

        void IManageTasks.Start(IEnumerable<string> args)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            Timer = new Timer(TimerCallback, null, 0, new TimeSpan(0, 0, 5).Milliseconds);
        }

        void IManageTasks.Stop()
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            Stop();
        }

        private static void TimerCallback(object state)
        {
            var log = LogManager.GetLogger(typeof(TaskManager));
            if (log.IsDebugEnabled)
            {
                log.Debug(new StackFrame().GetMethod().Name);
            }
        }

        private void Dispose(bool disposing)
        {
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
            if (_log.IsDebugEnabled)
            {
                _log.Debug(new StackFrame().GetMethod().Name);
            }

            if (null == Timer)
            {
                return;
            }

            Timer.Dispose();
            Timer = null;
        }
    }
}