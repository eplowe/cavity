namespace Cavity.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using Cavity.Diagnostics;

    [XmlRoot("directory.create")]
    public sealed class DirectoryCreateCommand : Command
    {
        public DirectoryCreateCommand()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
        }

        public DirectoryCreateCommand(bool unidirectional)
            : base(unidirectional)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "unidirectional={0}".FormatWith(unidirectional));
#endif
        }

        public DirectoryCreateCommand(string path)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "path=\"{0}\"".FormatWith(path));
#endif
            Path = path;
        }

        public DirectoryCreateCommand(string path, 
                                      bool unidirectional)
            : base(unidirectional)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "path=\"{0}\" unidirectional={1}".FormatWith(path, unidirectional));
#endif
            Path = path;
        }

        public DirectoryCreateCommand(DirectoryInfo directory)
        {
            Path = null == directory ? null : directory.FullName;
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "directory.FullName=\"{0}\"".FormatWith(Path));
#endif
        }

        public DirectoryCreateCommand(DirectoryInfo directory, 
                                      bool unidirectional)
            : base(unidirectional)
        {
            Path = null == directory ? null : directory.FullName;
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "directory.FullName=\"{0}\" unidirectional={1}".FormatWith(Path, unidirectional));
#endif
        }

        public string Path { get; set; }

        public override bool Act()
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Path=\"{0}\" Unidirectional={1}".FormatWith(Path, Unidirectional));
#endif
            var dir = new DirectoryInfo(Path);
            if (dir.Exists)
            {
                Undo = false;
            }
            else
            {
                dir.Create();
                Undo = !Unidirectional;
            }

            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            Path = reader.GetAttribute("path");

            base.ReadXml(reader);
        }

        public override bool Revert()
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Undo={0} Path=\"{1}\"".FormatWith(Undo, Path));
#endif
            if (Undo)
            {
                var dir = new DirectoryInfo(Path);
                dir.Delete();
            }

            return true;
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            if (!string.IsNullOrEmpty(Path))
            {
                writer.WriteAttributeString("path", Path);
            }

            base.WriteXml(writer);
        }
    }
}