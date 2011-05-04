namespace Cavity.Commands
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("file.copy")]
    public sealed class FileCopyCommand : Command
    {
        public FileCopyCommand()
        {
        }

        public FileCopyCommand(bool unidirectional)
            : base(unidirectional)
        {
        }

        public FileCopyCommand(string source,
                               string destination)
        {
            Source = source;
            Destination = destination;
        }

        public FileCopyCommand(string source,
                               string destination,
                               bool unidirectional)
            : base(unidirectional)
        {
            Source = source;
            Destination = destination;
        }

        public FileCopyCommand(FileInfo source,
                               FileInfo destination)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
        }

        public FileCopyCommand(FileInfo source,
                               FileInfo destination,
                               bool unidirectional)
            : base(unidirectional)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
        }

        public string Destination { get; set; }

        public string Source { get; set; }

        public override bool Act()
        {
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