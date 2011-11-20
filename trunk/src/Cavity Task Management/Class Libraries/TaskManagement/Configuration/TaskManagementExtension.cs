namespace Cavity.Configuration
{
    using System.Configuration;
    using System.IO;

    public sealed class TaskManagementExtension : ConfigurationElement
    {
        public TaskManagementExtension()
        {
            Properties.Add(ConfigurationProperty<DirectoryInfo>.Item("directory"));
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