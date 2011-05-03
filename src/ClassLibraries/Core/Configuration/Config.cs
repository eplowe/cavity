namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;

    public static class Config
    {
        [ThreadStatic]
        private static Dictionary<Type, object> _mock;

        public static void ClearMock()
        {
            _mock = null;
        }

        public static void SetMock(object value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            _mock = _mock ?? new Dictionary<Type, object>();

            _mock.Add(value.GetType(), value);
        }

        public static T ExeSection<T>() where T : ConfigurationSection, new()
        {
            return ExeSection<T>(Assembly.GetEntryAssembly());
        }

        public static T ExeSection<T>(Assembly assembly) where T : ConfigurationSection, new()
        {
            var type = typeof(T);
            if (null != _mock && _mock.ContainsKey(type))
            {
                return (T)_mock[type];
            }

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