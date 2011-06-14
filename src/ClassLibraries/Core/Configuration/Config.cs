namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;
#if !NET20
    using Cavity.Diagnostics;
#endif

    public static class Config
    {
#if !NET20
        private static HashSet<ConfigXml> _xml;
#endif

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

        public static T Section<T>(string sectionName) where T : ConfigurationSection, new()
        {
            if (null == sectionName)
            {
                throw new ArgumentNullException("sectionName");
            }

            if (0 == sectionName.Length)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }

            return ConfigurationManager.GetSection(sectionName) as T;
        }

        public static T SectionHandler<T>(string sectionName) where T : IConfigurationSectionHandler
        {
            if (null == sectionName)
            {
                throw new ArgumentNullException("sectionName");
            }

            if (0 == sectionName.Length)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }

            return (T)ConfigurationManager.GetSection(sectionName);
        }

#if !NET20
        public static T Xml<T>() where T : new()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            return Xml<T>(typeof(T).Assembly);
        }

        public static T Xml<T>(Assembly assembly) where T : new()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly");
            }

            return Xml<T>(new FileInfo(assembly.Location + ".xml"));
        }

        public static T Xml<T>(FileInfo file) where T : new()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);

            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (null == _xml)
            {
                _xml = new HashSet<ConfigXml>();
            }

            _xml.RemoveWhere(x => x.Changed);
            var xml = _xml.Where(x => string.Equals(x.Info.FullName, file.FullName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (null == xml)
            {
                xml = ConfigXml.Load<T>(file);
                _xml.Add(xml);
            }

            return (T)xml.Value;
        }
#endif
    }
}