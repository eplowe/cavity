namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Reflection;

    public sealed class TaskManagementConfiguration : ConfigurationSection
    {
        private static readonly ConfigurationProperty _refreshRate = new ConfigurationProperty("refreshRate",
                                                                                               typeof(TimeSpan),
                                                                                               TimeSpan.FromSeconds(5),
                                                                                               ConfigurationPropertyOptions.IsRequired);

        public TaskManagementConfiguration()
        {
            Properties.Add(_refreshRate);
        }

        public static TaskManagementConfiguration Exe
        {
            get
            {
                return GetExeConfiguration("cavity", "taskManagement") ?? new TaskManagementConfiguration();
            }
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

        private static TaskManagementConfiguration GetExeConfiguration(string groupName,
                                                                       string sectionName)
        {
            var assembly = Assembly.GetEntryAssembly();
            if (null == assembly)
            {
                return null;
            }

            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = assembly.Location + ".config"
            };

            var group = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None).GetSectionGroup(groupName);
            if (null == group)
            {
                return null;
            }

            return group.Sections[sectionName] as TaskManagementConfiguration;
        }
    }
}