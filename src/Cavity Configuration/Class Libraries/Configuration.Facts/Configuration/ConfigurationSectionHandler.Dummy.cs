namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Xml;

    public sealed class DummyConfigurationSectionHandler : IConfigurationSectionHandler
    {
        object IConfigurationSectionHandler.Create(object parent,
                                                   object configContext,
                                                   XmlNode section)
        {
            try
            {
                if (null == section)
                {
                    throw new XmlException("The dummy section is not configured.");
                }

                return new DummyConfigurationSectionHandler();
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException(exception.Message, exception, section);
            }
        }
    }
}