namespace Cavity.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;
    using Cavity.Diagnostics;

    public static class Config
    {
        private static readonly Dictionary<Type, object> _types = new Dictionary<Type, object>();

#if NET20
        private static List<ConfigXml> _xml;
#else
        private static HashSet<ConfigXml> _xml;
#endif

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public static void Clear<T>()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            _types.Remove(typeof(T));
        }

        public static T ExeSection<T>() where T : ConfigurationSection, new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

            return ExeSection<T>(Assembly.GetEntryAssembly());
        }

        public static T ExeSection<T>(Assembly assembly) where T : ConfigurationSection, new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly");
            }

            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

            var fileMap = new ExeConfigurationFileMap
            {
#if NET20
                ExeConfigFilename = assembly.Location + ".config"
#else
                ExeConfigFilename = assembly.Location.Append(".config")
#endif
            };

            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var section = Section<T>(config.Sections);
            if (null != section)
            {
                return section;
            }

            foreach (ConfigurationSectionGroup group in config.SectionGroups)
            {
                section = Section<T>(group.Sections);
                if (null == section)
                {
                    continue;
                }

                return section;
            }

            return default(T);
        }

        public static T Section<T>(string sectionName) where T : ConfigurationSection, new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == sectionName)
            {
                throw new ArgumentNullException("sectionName");
            }

            if (0 == sectionName.Length)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }

            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

            return ConfigurationManager.GetSection(sectionName) as T;
        }

        public static T SectionHandler<T>(string sectionName) where T : IConfigurationSectionHandler
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == sectionName)
            {
                throw new ArgumentNullException("sectionName");
            }

            if (0 == sectionName.Length)
            {
                throw new ArgumentOutOfRangeException("sectionName");
            }

            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

            return (T)ConfigurationManager.GetSection(sectionName);
        }

        public static void Set<T>(T obj)
        {
            Clear<T>();
            _types.Add(typeof(T), obj);
        }

        public static T Xml<T>() where T : new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

            return Xml<T>(typeof(T).Assembly);
        }

        public static T Xml<T>(Assembly assembly) where T : new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly");
            }

            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

#if NET20
            return Xml<T>(new FileInfo(assembly.Location + ".xml"));
#else
            return Xml<T>(new FileInfo(assembly.Location.Append(".xml")));
#endif
        }

        public static T Xml<T>(FileInfo file) where T : new()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (_types.ContainsKey(typeof(T)))
            {
                return (T)_types[typeof(T)];
            }

#if NET20
            _xml = _xml ?? new List<ConfigXml>();
            var count = _xml.Count;
            for (var i = 0; i < count; i++)
            {
                var item = _xml[count - 1 - i];
                if (item.Changed)
                {
                    _xml.Remove(item);
                }
            }

            ConfigXml xml = null;
            foreach (var item in _xml)
            {
                if (string.Equals(item.Info.FullName, file.FullName, StringComparison.OrdinalIgnoreCase))
                {
                    xml = item;
                    break;
                }
            }
#else
            _xml = _xml ?? new HashSet<ConfigXml>();
            _xml.RemoveWhere(x => x.Changed);
            var xml = _xml.FirstOrDefault(x => string.Equals(x.Info.FullName, file.FullName, StringComparison.OrdinalIgnoreCase));
#endif
            if (null == xml)
            {
                xml = ConfigXml.Load<T>(file);
                _xml.Add(xml);
            }

            return (T)xml.Value;
        }

#if NET20
        private static T Section<T>(ConfigurationSectionCollection sections) where T : ConfigurationSection
#else
        private static T Section<T>(IEnumerable sections) where T : ConfigurationSection
#endif
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
#if NET20
            foreach (var item in sections)
            {
                var section = item as T;
                if (null == section)
                {
                    continue;
                }

                return section;
            }

            return default(T);
#else
            return sections.OfType<T>().FirstOrDefault();
#endif
        }
    }
}