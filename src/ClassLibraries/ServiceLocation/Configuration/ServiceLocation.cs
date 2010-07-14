namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;
    using Cavity.Properties;

    public sealed class ServiceLocation : IConfigurationSectionHandler
    {
        public static ISetLocatorProvider Settings()
        {
            return Settings("serviceLocation");
        }

        public static ISetLocatorProvider Settings(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as ISetLocatorProvider;
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            ISetLocatorProvider result;

            try
            {
                if (null == section)
                {
                    throw new XmlException(Resources.ServiceLocation_NullSectionMessage);
                }

                var attribute = section.Attributes["type"];
                if (null == attribute)
                {
                    throw new XmlException(Resources.ServiceLocation_TypeAttributeRequiredMessage);
                }

                var type = Type.GetType(attribute.Value);
                if (null == type)
                {
                    throw new XmlException(Resources.ServiceLocation_TypeAttributeRequiredMessage);
                }

                result = Activator.CreateInstance(type) as ISetLocatorProvider;
                if (null == result)
                {
                    throw new XmlException(Resources.ServiceLocation_TypeInterfaceRequiredMessage);
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