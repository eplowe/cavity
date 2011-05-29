﻿namespace Cavity.IO
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
            Trace.WriteIf(Tracing.Enabled, string.Empty);
        }

        public DirectoryCreateCommand(bool unidirectional)
            : base(unidirectional)
        {
            Trace.WriteIf(Tracing.Enabled, "unidirectional={0}".FormatWith(unidirectional));
        }

        public DirectoryCreateCommand(string path)
        {
            Trace.WriteIf(Tracing.Enabled, "path=\"{0}\"".FormatWith(path));
            Path = path;
        }

        public DirectoryCreateCommand(string path,
                                      bool unidirectional)
            : base(unidirectional)
        {
            Trace.WriteIf(Tracing.Enabled, "path=\"{0}\" unidirectional={1}".FormatWith(path, unidirectional));
            Path = path;
        }

        public DirectoryCreateCommand(DirectoryInfo directory)
        {
            Path = null == directory ? null : directory.FullName;
            Trace.WriteIf(Tracing.Enabled, "directory.FullName=\"{0}\"".FormatWith(Path));
        }

        public DirectoryCreateCommand(DirectoryInfo directory,
                                      bool unidirectional)
            : base(unidirectional)
        {
            Path = null == directory ? null : directory.FullName;
            Trace.WriteIf(Tracing.Enabled, "directory.FullName=\"{0}\" unidirectional={1}".FormatWith(Path, unidirectional));
        }

        public string Path { get; set; }

        public override bool Act()
        {
            Trace.WriteIf(Tracing.Enabled, "Path=\"{0}\" Unidirectional={1}".FormatWith(Path, Unidirectional));
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
            Trace.WriteIf(Tracing.Enabled, "Undo={0} Path=\"{1}\"".FormatWith(Undo, Path));
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