namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using Cavity.Diagnostics;

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
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Properties.Add(_directory);
        }

        public DirectoryConfigurationElement(string name,
                                             DirectoryInfo directory)
            : this()
        {
            Name = name;
            Directory = directory;
        }

        public DirectoryInfo Directory
        {
            get
            {
                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                return (DirectoryInfo)this["directory"];
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                this["directory"] = value;
            }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }
    }
}