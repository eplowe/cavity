namespace Cavity.Collections
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot("add")]
    public sealed class DirectoryItem : PathItem,
                                        IEquatable<DirectoryItem>
    {
        [XmlIgnore]
        public DirectoryInfo Info
        {
            get
            {
                return new DirectoryInfo(Value);
            }
        }

        public bool Equals(DirectoryItem other)
        {
            if (null == other)
            {
                return false;
            }

            return Name == other.Name && Value == other.Value;
        }
    }
}