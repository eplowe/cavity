namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics;
    using Cavity.Diagnostics;

    public sealed class RedirectionConfigurationElement<T> : ConfigurationElement
    {
        public RedirectionConfigurationElement()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            Properties.Add(ConfigurationProperty<T>.Item("from"));
            Properties.Add(ConfigurationProperty<T>.Item("to"));
        }

        public RedirectionConfigurationElement(T from,
                                               T to)
            : this()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);

            From = from;
            To = to;
        }

        public T From
        {
            get
            {
                return (T)this["from"];
            }

            set
            {
                this["from"] = value;
            }
        }

        public T To
        {
            get
            {
                return (T)this["to"];
            }

            set
            {
                this["to"] = value;
            }
        }
    }
}