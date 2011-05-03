namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;

    public sealed class DirectoryConfigurationElement : ConfigurationElement
    {
        private static readonly TypeConverter _converter = new DirectoryInfoConverter();

        private static readonly ConfigurationValidatorBase _validator = new DirectoryInfoValidator();

        private static readonly ConfigurationProperty _directory = new ConfigurationProperty("directory",
                                                                                             typeof(DirectoryInfo),
                                                                                             null,
                                                                                             _converter,
                                                                                             _validator,
                                                                                             ConfigurationPropertyOptions.IsRequired);

        public DirectoryConfigurationElement()
        {
            Properties.Add(_directory);
        }

        public DirectoryConfigurationElement(DirectoryInfo directory)
            : this()
        {
            Directory = directory;
        }

        public DirectoryInfo Directory
        {
            get
            {
                return new DirectoryInfo((string)this["directory"]);
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this["directory"] = value.FullName;
            }
        }
    }
}