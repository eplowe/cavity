namespace Cavity.Configuration
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Security.Permissions;

    using Cavity.Diagnostics;
    using Cavity.IO;

    public class ConfigXml
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private ConfigXml(FileInfo file)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == file)
            {
                return;
            }

            if (null == file.Directory)
            {
                return;
            }

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

        public bool Changed { get; protected set; }

        public FileInfo Info { get; protected set; }

        public object Value { get; protected set; }

        private FileSystemWatcher Watcher { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public static ConfigXml Load<T>(FileInfo file)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "file.FullName=\"{0}\"".FormatWith(file.FullName));
#endif
            return new ConfigXml(file)
                       {
#if NET20
                Value = file.Exists
                            ? StringExtensionMethods.XmlDeserialize<T>(FileInfoExtensionMethods.ReadToEnd(file))
                            : default(T)
#else
                           Value = file.Exists
                                       ? file.ReadToEnd().XmlDeserialize<T>()
                                       : default(T)
#endif
                       };
        }

        protected virtual void OnChanged(object source, 
                                         FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Changed = true;
        }

        protected virtual void OnCreated(object source, 
                                         FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Changed = true;
        }

        protected virtual void OnDeleted(object source, 
                                         FileSystemEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Changed = true;
        }

        protected virtual void OnRenamed(object source, 
                                         RenamedEventArgs e)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Changed = true;
        }
    }
}