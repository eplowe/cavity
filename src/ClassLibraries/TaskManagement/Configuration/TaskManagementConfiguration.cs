namespace Cavity.Configuration
{
    using System;
    using System.Configuration;

    public sealed class TaskManagementConfiguration : ConfigurationSection
    {
        private static readonly ConfigurationProperty _refreshRate = new ConfigurationProperty("refreshRate",
                                                                                               typeof(TimeSpan),
                                                                                               TimeSpan.FromSeconds(1),
                                                                                               ConfigurationPropertyOptions.IsRequired);

        public TaskManagementConfiguration()
        {
            Properties.Add(_refreshRate);
        }

        [TimeSpanValidator(MinValueString = "0:0:1", MaxValueString = "24:00:0", ExcludeRange = false)]
        public TimeSpan RefreshRate
        {
            get
            {
                return (TimeSpan)this["refreshRate"];
            }

            set
            {
                this["refreshRate"] = value;
            }
        }
    }
}