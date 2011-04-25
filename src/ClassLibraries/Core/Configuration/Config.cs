﻿namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Reflection;

    public static class Config
    {
        public static T ExeSection<T>() where T : ConfigurationSection, new()
        {
            var assembly = Assembly.GetEntryAssembly();
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