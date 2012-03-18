namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    using Cavity.Properties;

    public sealed class FileRepositoryConfiguration : IConfigurationSectionHandler
    {
        [ThreadStatic]
        private static DirectoryInfo _mock;

        public static DirectoryInfo Mock
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

        public static DirectoryInfo Directory()
        {
            return _mock ?? Directory("cavity.repository");
        }

        public static DirectoryInfo Directory(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as DirectoryInfo;
        }

        public object Create(object parent, 
                             object configContext, 
                             XmlNode section)
        {
            try
            {
                if (null == section)
                {
                    throw new XmlException(Resources.FileRepositoyConfiguration_NullSectionMessage);
                }

                var attributes = section.Attributes;
                if (null == attributes)
                {
                    throw new XmlException(Resources.FileRepositoyConfiguration_DirectoryAttributeRequiredMessage);
                }

                var attribute = attributes["directory"];
                if (null == attribute)
                {
                    throw new XmlException(Resources.FileRepositoyConfiguration_DirectoryAttributeRequiredMessage);
                }

                if (null == attribute.Value)
                {
                    throw new XmlException(Resources.FileRepositoyConfiguration_DirectoryAttributeRequiredMessage);
                }

                return new DirectoryInfo(attribute.Value);
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException(exception.Message, exception, section);
            }
        }
    }
}