namespace Cavity.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using Cavity.Diagnostics;

    [XmlRoot("file.copy")]
    public sealed class FileCopyCommand : Command
    {
        public FileCopyCommand()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
        }

        public FileCopyCommand(bool unidirectional)
            : base(unidirectional)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "unidirectional={0}".FormatWith(unidirectional));
#endif
        }

        public FileCopyCommand(string source, 
                               string destination)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "source=\"{0}\" destination=\"{1}\"".FormatWith(source, destination));
#endif
            Source = source;
            Destination = destination;
        }

        public FileCopyCommand(string source, 
                               string destination, 
                               bool unidirectional)
            : base(unidirectional)
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "source=\"{0}\" destination=\"{1}\" unidirectional={2}".FormatWith(source, destination, unidirectional));
#endif
            Source = source;
            Destination = destination;
        }

        public FileCopyCommand(FileInfo source, 
                               FileInfo destination)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "source.FullName=\"{0}\" destination.FullName=\"{1}\"".FormatWith(Source, Destination));
#endif
        }

        public FileCopyCommand(FileInfo source, 
                               FileInfo destination, 
                               bool unidirectional)
            : base(unidirectional)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "source.FullName=\"{0}\" destination.FullName=\"{1}\" unidirectional={2}".FormatWith(Source, Destination, unidirectional));
#endif
        }

        public string Destination { get; set; }

        public string Source { get; set; }

        public override bool Act()
        {
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Source=\"{0}\" Destination=\"{1}\" Unidirectional={2}".FormatWith(Source, Destination, Unidirectional));
#endif
            File.Copy(Source, Destination);
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
#if !NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Undo={0} Destination=\"{1}\"".FormatWith(Undo, Destination));
#endif
            if (Undo)
            {
                File.Delete(Destination);
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