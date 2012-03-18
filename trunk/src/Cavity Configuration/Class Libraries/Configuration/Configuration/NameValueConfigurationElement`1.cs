namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics;

    using Cavity.Diagnostics;

    public sealed class NameValueConfigurationElement<T> : ConfigurationElement
    {
        public NameValueConfigurationElement()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            Properties.Add(ConfigurationProperty<T>.Item("value"));
        }

        public NameValueConfigurationElement(string name, 
                                             T value)
            : this()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            Name = name;
            Value = value;
        }

        [ConfigurationProperty("name", IsRequired = false)]
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

        public T Value
        {
            get
            {
                return (T)this["value"];
            }

            set
            {
                this["value"] = value;
            }
        }
    }
}