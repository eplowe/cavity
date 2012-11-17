namespace Cavity.Collections
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot("add")]
    public sealed class FileItem : PathItem,
                                   IEquatable<FileItem>
    {
        [XmlIgnore]
        public FileInfo Info
        {
            get
            {
                return new FileInfo(Value);
            }
        }

        public bool Equals(FileItem other)
        {
            if (null == other)
            {
                return false;
            }

            return Name == other.Name && Value == other.Value;
        }
    }
}