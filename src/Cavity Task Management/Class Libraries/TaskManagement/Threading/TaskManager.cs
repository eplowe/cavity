namespace Cavity.Threading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Cavity.Configuration;
    using Cavity.Diagnostics;
    using Cavity.Properties;

    public class TaskManager : DisposableObject, 
                               IManageTasks
    {
        public TaskManager()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            Cancellation = new CancellationTokenSource();
        }

        private static AggregateCatalog AllCatalogs
        {
            get
            {
                var aggregate = new AggregateCatalog();
                if (null != Assembly.GetEntryAssembly())
                {
                    aggregate.Catalogs.Add(EntryAssemblyCatalog);
                }

                var extensions = Config.ExeSection<TaskManagementSettings>(typeof(TaskManager).Assembly).Extensions;
                if (null == extensions)
                {
                    Trace.WriteLineIf(Tracing.Is.TraceInfo, Resources.TaskManager_NullExtensions);
                    return aggregate;
                }

                foreach (TaskManagementExtension extension in extensions)
                {
                    if (!extension.Directory.Exists)
                    {
                        Trace.WriteLineIf(Tracing.Is.TraceInfo, Resources.TaskManager_ExtensionsDirectoryNotExists);
                        continue;
                    }

                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, Resources.TaskManager_ExtensionsDirectoryFullName.FormatWith(extension.Directory.FullName));
                    var catalog = new DirectoryCatalog(extension.Directory.FullName);
                    aggregate.Catalogs.Add(catalog);
                }

                return aggregate;
            }
        }

        private static DirectoryCatalog EntryAssemblyCatalog
        {
            get
            {
                var file = new FileInfo(Assembly.GetEntryAssembly().Location);

                // ReSharper disable PossibleNullReferenceException
                return new DirectoryCatalog(file.Directory.FullName);

                // ReSharper restore PossibleNullReferenceException
            }
        }

        private static long Period
        {
            get
            {
                var settings = Config.ExeSection<TaskManagementSettings>(typeof(TaskManagementSettings).Assembly);

                return Convert.ToInt64(settings.RefreshRate.TotalMilliseconds);
            }
        }

        private CancellationTokenSource Cancellation { get; set; }

        private Timer Timer { get; set; }

        public virtual void Continue()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            Timer.Change(0, Period);
        }

        public virtual void Pause()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public virtual void Shutdown()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            Stop();
        }

        public virtual void Start(IEnumerable<string> args)
        {
            var combo = string.Empty;
            if (null != args)
            {
                combo = args.Aggregate(combo, (x, 
                                               arg) => "{0}{1}\"{2}\"".FormatWith(x, null == x ? string.Empty : ", ", arg));
            }

            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "args={0}".FormatWith(combo));
            Timer = new Timer(TimerCallback, null, 0, Period);
        }

        public virtual void Stop()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == Timer)
            {
                return;
            }

            Timer.Dispose();
            Timer = null;
            Cancellation.Cancel();
        }

        protected override void OnDispose()
        {
            try
            {
                Stop();
            }
            finally
            {
                Cancellation.Dispose();
                Cancellation = null;
            }
        }

        protected virtual void TimerCallback(object state)
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, "state={0}".FormatWith(state));
            RunTasks();
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is to be robust in the face of exceptions.")]
        private void RunTasks()
        {
            Trace.WriteLineIf(Tracing.Is.TraceVerbose, string.Empty);
            try
            {
                foreach (var export in new CompositionContainer(AllCatalogs).GetExports<ITask>())
                {
                    var task = export.Value;
                    if (null == task)
                    {
                        continue;
                    }

                    Trace.WriteLineIf(Tracing.Is.TraceVerbose, Resources.TaskManager_TaskGetTypeFullName.FormatWith(task.GetType().FullName));
                    var factory = new TaskFactory(Cancellation.Token, 
                                                  task.CreationOptions, 
                                                  task.ContinuationOptions, 
                                                  TaskScheduler.Default);
                    factory.StartNew(() => task.Run(Cancellation.Token));
                    TaskCounter.Increment();
                }
            }
            catch (ReflectionTypeLoadException exception)
            {
                Trace.TraceError("{0}".FormatWith(exception));
                Trace.Indent();
                if (null != exception.LoaderExceptions)
                {
                    foreach (var item in exception.LoaderExceptions)
                    {
                        Trace.TraceError("{0}".FormatWith(item));
                    }
                }

                Trace.Unindent();
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}".FormatWith(exception));
            }
        }
    }
}