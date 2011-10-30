namespace Cavity.Configuration
{
    using System;
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
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            var extension = element as TaskManagementExtension;
            if (null == extension)
            {
                throw new ArgumentOutOfRangeException("element");
            }

            return extension.Directory.FullName;
        }
    }
}