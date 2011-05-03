namespace Cavity.Commands
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("file.move")]
    public sealed class FileMoveCommand : Command
    {
        public FileMoveCommand()
        {
        }

        public FileMoveCommand(bool unidirectional)
            : base(unidirectional)
        {
        }

        public FileMoveCommand(string source,
                               string destination)
            : base(false)
        {
            Source = source;
            Destination = destination;
        }

        public FileMoveCommand(string source,
                               string destination,
                               bool unidirectional)
            : base(unidirectional)
        {
            Source = source;
            Destination = destination;
        }

        public FileMoveCommand(FileInfo source,
                               FileInfo destination)
            : base(false)
        {
            Source = null == source ? null : source.FullName;
            Destination = null == destination ? null : destination.FullName;
        }

        public FileMoveCommand(FileInfo source,
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