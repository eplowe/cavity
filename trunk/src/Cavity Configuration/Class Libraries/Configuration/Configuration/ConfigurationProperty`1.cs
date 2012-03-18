namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    public static class ConfigurationProperty<T>
    {
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static ConfigurationProperty Item(string name)
        {
            return Item(name, ConfigurationPropertyOptions.IsRequired);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static ConfigurationProperty Item(string name, 
                                                 ConfigurationPropertyOptions options)
        {
            return new ConfigurationProperty(name, 
                                             typeof(T), 
                                             null, 
                                             new ConfigurationConverter<T>(), 
                                             new ConfigurationValidator<T>(), 
                                             options);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static ConfigurationProperty Item(string name, 
                                                 T defaultValue)
        {
            return Item(name, defaultValue, ConfigurationPropertyOptions.IsRequired);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static ConfigurationProperty Item(string name, 
                                                 T defaultValue, 
                                                 ConfigurationPropertyOptions options)
        {
            return new ConfigurationProperty(name, 
                                             typeof(T), 
                                             defaultValue, 
                                             new ConfigurationConverter<T>(), 
                                             new ConfigurationValidator<T>(), 
                                             options);
        }
    }
}