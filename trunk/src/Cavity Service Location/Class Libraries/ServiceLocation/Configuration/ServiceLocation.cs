namespace Cavity.Configuration
{
    using System.Configuration;
    using System.Diagnostics;
    using Cavity.Diagnostics;

    public sealed class ServiceLocation : ConfigurationSection
    {
        private static readonly ConfigurationProperty _provider = new ConfigurationProperty("type",
                                                                                            typeof(ISetLocatorProvider),
                                                                                            null,
                                                                                            new SetLocatorProviderConverter(),
                                                                                            new SetLocatorProviderValidator(),
                                                                                            ConfigurationPropertyOptions.IsRequired);

        public ServiceLocation()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Properties.Add(_provider);
        }

        public ISetLocatorProvider Provider
        {
            get
            {
                return (ISetLocatorProvider)this["type"];
            }

            set
            {
                this["type"] = value;
            }
        }
    }
}