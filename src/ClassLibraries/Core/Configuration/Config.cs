namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
#if !NET20
    using Cavity.Diagnostics;
#endif

    public static class Config
    {
        public static T ExeSection<T>() where T : ConfigurationSection, new()
        {
#if !NET20
            Trace.WriteIf(Tracing.Enabled, string.Empty);
#endif
            return ExeSection<T>(Assembly.GetEntryAssembly());
        }

        public static T ExeSection<T>(Assembly assembly) where T : ConfigurationSection, new()
        {
#if !NET20
            Trace.WriteIf(Tracing.Enabled, string.Empty);
#endif
            if (null == assembly)
            {
                return Activator.CreateInstance<T>();
            }

            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = assembly.Location + ".config"
            };

            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            foreach (ConfigurationSectionGroup group in config.SectionGroups)
            {
                foreach (var item in group.Sections)
                {
                    var section = item as T;
                    if (null == section)
                    {
                        continue;
                    }

                    return section;
                }
            }

            return Activator.CreateInstance<T>();
        }
    }
}