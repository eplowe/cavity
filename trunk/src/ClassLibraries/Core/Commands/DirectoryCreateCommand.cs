namespace Cavity.Commands
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("directory.create")]
    public sealed class DirectoryCreateCommand : Command
    {
        public DirectoryCreateCommand()
        {
        }

        public DirectoryCreateCommand(bool unidirectional)
            : base(unidirectional)
        {
        }

        public DirectoryCreateCommand(string path)
            : base(false)
        {
            Path = path;
        }

        public DirectoryCreateCommand(string path,
                                      bool unidirectional)
            : base(unidirectional)
        {
            Path = path;
        }

        public DirectoryCreateCommand(DirectoryInfo directory)
            : base(false)
        {
            Path = null == directory ? null : directory.FullName;
        }

        public DirectoryCreateCommand(DirectoryInfo directory,
                                      bool unidirectional)
            : base(unidirectional)
        {
            Path = null == directory ? null : directory.FullName;
        }

        public string Path { get; set; }

        public override bool Act()
        {
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