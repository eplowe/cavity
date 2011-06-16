namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;

    public sealed class PathConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("directories", IsRequired = false, IsDefaultCollection = true)]
        public DirectoryConfigurationElementCollection Directories
        {
            get
            {
                return (DirectoryConfigurationElementCollection)this["directories"];
            }
        }

        [ConfigurationProperty("files", IsRequired = false, IsDefaultCollection = true)]
        public FileConfigurationElementCollection Files
        {
            get
            {
                return (FileConfigurationElementCollection)this["files"];
            }
        }

        public DirectoryInfo Directory(string name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            foreach (var item in Directories)
            {
                if (item.Name.Equals(name, StringComparison.Ordinal))
                {
                    return item.Directory;
                }
            }

            return null;
        }

        public FileInfo File(string name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            foreach (var item in Files)
            {
                if (item.Name.Equals(name, StringComparison.Ordinal))
                {
                    return item.File;
                }
            }

            return null;
        }
    }
}