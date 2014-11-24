namespace Cavity.Collections
{
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Xml.Serialization;
    using Cavity.Collections.Generic;

    [XmlRoot("files")]
    public sealed class FileCollection : XmlSerializableCollection<FileItem>
    {
        public FileInfo this[string name]
        {
            get
            {
#if NET20
                foreach (var item in this)
                {
                    if (item.Name == name)
                    {
                        return item.Info;
                    }
                }
#else
                foreach (var item in this.Where(item => item.Name == name))
                {
                    return item.Info;
                }
#endif
                throw new KeyNotFoundException(name);
            }
        }
    }
}