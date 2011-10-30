namespace Cavity.Configuration
{
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;

    public sealed class TaskManagementExtension : ConfigurationElement
    {
        private static readonly TypeConverter _converter = new DirectoryInfoConverter();

        private static readonly ConfigurationProperty _directory = new ConfigurationProperty("directory",
                                                                                             typeof(DirectoryInfo),
                                                                                             null,
                                                                                             _converter,
                                                                                             _validator,
                                                                                             ConfigurationPropertyOptions.IsRequired);

        private static readonly ConfigurationValidatorBase _validator = new DirectoryInfoValidator();

        public TaskManagementExtension()
        {
            Properties.Add(_directory);
        }

        public DirectoryInfo Directory
        {
            get
            {
                return (DirectoryInfo)this["directory"];
            }

            set
            {
                this["directory"] = value;
            }
        }
    }
}