namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using Cavity.Diagnostics;

    public sealed class FileConfigurationElement : ConfigurationElement
    {
        private static readonly TypeConverter _converter = new FileInfoConverter();

        private static readonly ConfigurationValidatorBase _validator = new FileInfoValidator();

        private static readonly ConfigurationProperty _file = new ConfigurationProperty("file",
                                                                                        typeof(FileInfo),
                                                                                        null,
                                                                                        _converter,
                                                                                        _validator,
                                                                                        ConfigurationPropertyOptions.IsRequired);

        public FileConfigurationElement()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Properties.Add(_file);
        }

        public FileConfigurationElement(FileInfo file)
            : this()
        {
            File = file;
        }

        public FileInfo File
        {
            get
            {
                Trace.WriteIf(Tracing.Enabled, string.Empty);
                return (FileInfo)this["file"];
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                Trace.WriteIf(Tracing.Enabled, string.Empty);
                this["file"] = value;
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