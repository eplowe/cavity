namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using Cavity.Properties;

    public sealed class ConfiguredDirectory : IConfigurationSectionHandler
    {
        [ThreadStatic]
        private static IDictionary<string, DirectoryInfo> _mock;

        public static IDictionary<string, DirectoryInfo> Mock
        {
            get
            {
                return _mock;
            }

            set
            {
                _mock = value;
            }
        }

        public static DirectoryInfo Item(string name)
        {
            var settings = _mock ?? Settings();

            return settings[name];
        }

        public static IDictionary<string, DirectoryInfo> Settings()
        {
            return Settings("directories");
        }

        public static IDictionary<string, DirectoryInfo> Settings(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as IDictionary<string, DirectoryInfo>;
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var result = new Dictionary<string, DirectoryInfo>();

            try
            {
                if (null == section)
                {
                    throw new XmlException(Resources.ConfiguredList_NullSectionMessage);
                }

                foreach (XmlNode directory in section.ChildNodes)
                {
                    if (null == directory)
                    {
                        continue;
                    }

                    if (null == directory.Attributes)
                    {
                        continue;
                    }

                    var name = directory.Attributes["name"].Value;
                    result.Add(name, new DirectoryInfo(directory.InnerText));
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException(exception.Message, exception, section);
            }

            return result;
        }
    }
}