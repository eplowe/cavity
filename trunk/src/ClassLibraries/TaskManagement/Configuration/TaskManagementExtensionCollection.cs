namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "Only temporary.")]
    public sealed class TaskManagementExtensionCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "extensions";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TaskManagementExtension();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var extension = element as TaskManagementExtension;

            return extension.Directory.FullName;
        }
    }
}