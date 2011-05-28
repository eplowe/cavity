namespace Cavity.Configuration
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Diagnostics;
    using Cavity.IO;

    public sealed class ConfigXml
    {
        private ConfigXml(FileInfo file)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Info = file;
            Watcher = new FileSystemWatcher(file.Directory.FullName, file.Name)
            {
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            Watcher.Changed += OnChanged;
            Watcher.Deleted += OnDeleted;
            Watcher.Created += OnCreated;
            Watcher.Renamed += OnRenamed;
        }

        public bool Changed { get; set; }

        public FileInfo Info { get; set; }

        public object Value { get; set; }

        private FileSystemWatcher Watcher { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public static ConfigXml Load<T>(FileInfo file)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            return new ConfigXml(file)
            {
                Value = file.Exists
                            ? file.ReadToEnd().XmlDeserialize<T>()
                            : default(T)
            };
        }

        private void OnChanged(object source,
                               FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Changed = true;
        }

        private void OnCreated(object source,
                               FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Changed = true;
        }

        private void OnDeleted(object source,
                               FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Changed = true;
        }

        private void OnRenamed(object source,
                               RenamedEventArgs e)
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Changed = true;
        }
    }
}