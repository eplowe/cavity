namespace Cavity.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Cavity.Diagnostics;

    [XmlRoot("file.move")]
    public sealed class FileMoveCommand : Command
    {
        public FileMoveCommand()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
        }

        public FileMoveCommand(bool unidirectional)
            : base(unidirectional)
        {
            Trace.WriteIf(Tracing.Enabled, "unidirectional={0}".FormatWith(unidirectional));
        }

        public FileMoveCommand(string source,
                               string destination)
            : base(false)
        {
            Trace.WriteIf(Tracing.Enabled, "source=\"{0}\" destination=\"{1}\"".FormatWith(source, destination));
            Source = source;
            Destination = destination;
        }

        public FileMoveCommand(string source,
                               string destination,
                               bool unidirectional)
            : base(unidirectional)
        {
            Trace.WriteIf(Tracing.Enabled, "source=\"{0}\" destination=\"{1}\" unidirectional={2}".FormatWith(source, destination, unidirectional));
            Source = source;
            Destination = destination;
        }

        public FileMoveCommand(FileInfo source,
                               FileInfo destination)
            : base(false)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
            Trace.WriteIf(Tracing.Enabled, "source.FullName=\"{0}\" destination.FullName=\"{1}\"".FormatWith(Source, Destination));
        }

        public FileMoveCommand(FileInfo source,
                               FileInfo destination,
                               bool unidirectional)
            : base(unidirectional)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
            Trace.WriteIf(Tracing.Enabled, "source.FullName=\"{0}\" destination.FullName=\"{1}\" unidirectional={2}".FormatWith(Source, Destination, unidirectional));
        }

        public string Destination { get; set; }

        public string Source { get; set; }

        public override bool Act()
        {
            Trace.WriteIf(Tracing.Enabled, "Source=\"{0}\" Destination=\"{1}\" Unidirectional={2}".FormatWith(Source, Destination, Unidirectional));
            File.Move(Source, Destination);
            Undo = !Unidirectional;

            return true;
        }

        public override void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            Destination = reader.GetAttribute("destination");
            Source = reader.GetAttribute("source");

            base.ReadXml(reader);
        }

        public override bool Revert()
        {
            Trace.WriteIf(Tracing.Enabled, "Undo={0} Destination=\"{1}\" Source=\"{2}\"".FormatWith(Undo, Destination, Source));
            if (Undo)
            {
                File.Move(Destination, Source);
            }

            return true;
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            if (!string.IsNullOrEmpty(Destination))
            {
                writer.WriteAttributeString("destination", Destination);
            }

            if (!string.IsNullOrEmpty(Source))
            {
                writer.WriteAttributeString("source", Source);
            }

            base.WriteXml(writer);
        }
    }
}