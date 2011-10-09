namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
#if !NET20
    using System.Linq;
#endif

    public class PathConfigurationSection : ConfigurationSection
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

#if NET20
            foreach (var item in Directories)
            {
                if (item.Name.Equals(name, StringComparison.Ordinal))
                {
                    return item.Directory;
                }
            }

            return null;
#else
            return (from item in Directories
                    where item.Name.Equals(name, StringComparison.Ordinal)
                    select item.Directory).FirstOrDefault();
#endif
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

#if NET20
            foreach (var item in Files)
            {
                if (item.Name.Equals(name, StringComparison.Ordinal))
                {
                    return item.File;
                }
            }

            return null;
#else
            return (from item in Files
                    where item.Name.Equals(name, StringComparison.Ordinal)
                    select item.File).FirstOrDefault();
#endif
        }
    }
}