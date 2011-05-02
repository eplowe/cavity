namespace Cavity.Commands
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Cavity.Xml.Serialization;

    [XmlRoot("directory.create")]
    public sealed class DirectoryCreateCommand : IXmlSerializableCommand
    {
        public DirectoryCreateCommand()
        {
        }

        public DirectoryCreateCommand(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public bool Undo { get; set; }

        public static explicit operator DirectoryCreateCommand(DirectoryInfo dir)
        {
            return FromDirectoryInfo(dir);
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static DirectoryCreateCommand FromDirectoryInfo(DirectoryInfo dir)
        {
            return (null == dir) ? null : new DirectoryCreateCommand(dir.FullName);
        }

        public bool Act()
        {
            var dir = new DirectoryInfo(Path);
            if (dir.Exists)
            {
                Undo = false;
            }
            else
            {
                dir.Create();
                Undo = true;
            }

            return true;
        }

        public bool Revert()
        {
            if (Undo)
            {
                var dir = new DirectoryInfo(Path);
                dir.Delete();
            }

            return true;
        }

        public XmlSchema GetSchema()
        {
            throw new NotSupportedException();
        }

        public void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            Path = reader.GetAttribute("path");
            var attribute = reader.GetAttribute("undo");
            if (!string.IsNullOrEmpty(attribute))
            {
                Undo = XmlConvert.ToBoolean(attribute);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            if (!string.IsNullOrEmpty(Path))
            {
                writer.WriteAttributeString("path", Path);
            }

            writer.WriteAttributeString("undo", XmlConvert.ToString(Undo));
        }
    }
}