namespace Cavity.IO
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Cavity.Diagnostics;
    using Cavity.Dynamic;
    using Cavity.Models;
    using Cavity.Threading;

    public class FileReceiver<T> : ThreadedObject, IReceiveFile
        where T : IProcessFile, new()
    {
        private static readonly object _lock = new object();

        public FileReceiver()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Data = new DynamicData();
            Timer = new Timer(OnTimerCallback, null, 0, Period);
        }

        public dynamic Data { get; protected set; }

        public Timer Timer { get; protected set; }

        public FileSystemWatcher Watcher { get; protected set; }

        private static long Period
        {
            get
            {
                return Convert.ToInt64(new TimeSpan(0, 0, 0, 10).TotalMilliseconds);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "This is for type safety.")]
        public void SetDropFolderWatch(DirectoryInfo drop,
                                       string filter)
        {
            if (null == drop)
            {
                throw new ArgumentNullException("drop");
            }

            Watcher = new FileSystemWatcher(drop.FullName, filter)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            Watcher.Created += OnCreated;
            Watcher.Renamed += OnRenamed;
        }

        public virtual void Receive(string path)
        {
            Trace.WriteIf(Tracing.Is.TraceInfo, "path=\"{0}\"".FormatWith(path));
            if (null == path)
            {
                throw new ArgumentNullException("path");
            }

            if (0 == path.Length)
            {
                throw new ArgumentOutOfRangeException("path");
            }

            Receive(new FileInfo(path));
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Don't leak exceptions.")]
        public virtual void Receive(FileInfo file)
        {
            Trace.WriteIf(Tracing.Is.TraceInfo, string.Empty);
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            lock (_lock)
            {
                if (null != Timer)
                {
                    Timer.Change(Timeout.Infinite, Timeout.Infinite);
                    try
                    {
                        Activator.CreateInstance<T>().Process(file, Data);
                    }
                    catch (Exception exception)
                    {
                        Trace.TraceError("{0}", exception);
                    }
                    finally
                    {
                        Timer.Change(0, Period);
                    }
                }
            }
        }

        protected virtual void OnCreated(object source,
                                         FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null != e)
            {
                Receive(e.FullPath);
            }
        }

        protected override void OnDispose()
        {
            if (null != Timer)
            {
                Timer.Dispose();
                Timer = null;
            }

            if (null != Watcher)
            {
                Watcher.Dispose();
                Watcher = null;
            }
        }

        protected virtual void OnRenamed(object source,
                                         RenamedEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null != e)
            {
                Receive(e.FullPath);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Don't leak exceptions.")]
        protected virtual void OnTimerCallback(object state)
        {
            Trace.WriteLineIf(Tracing.Is.TraceInfo, "state={0}".FormatWith(state));
            try
            {
                if (null != Watcher)
                {
                    var files = new DirectoryInfo(Watcher.Path).GetFiles(Watcher.Filter, SearchOption.AllDirectories);
                    var file = null == files ? null : files.FirstOrDefault();
                    if (null != file)
                    {
                        Receive(file);
                    }
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError("{0}", exception);
            }
        }
    }
}