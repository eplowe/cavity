namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Threading;
    using System.Xml;
    using Cavity.Configuration;
    using Cavity.Data;
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
            RunTasks();
        }

        private static void RunTasks()
        {
            LoggingSignature.Debug();
            var aggregate = new AggregateCatalog();
            foreach (TaskManagementExtension extension in Config.ExeSection<TaskManagementSettings>().Extensions)
            {
                var catalog = new DirectoryCatalog(extension.Directory.FullName);
                aggregate.Catalogs.Add(catalog);
            }

            foreach (var export in new CompositionContainer(aggregate).GetExports<ITask>())
            {
                export.Value.Run();
            }
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