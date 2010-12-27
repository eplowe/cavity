namespace Cavity.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using Cavity.Properties;

    public sealed class ConfiguredList : IConfigurationSectionHandler
    {
        public static IEnumerable<string> Items(string name)
        {
            return Settings()[name];
        }

        public static IDictionary<string, IEnumerable<string>> Settings()
        {
            return Settings("lists");
        }

        public static IDictionary<string, IEnumerable<string>> Settings(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName) as IDictionary<string, IEnumerable<string>>;
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var result = new Dictionary<string, IEnumerable<string>>();

            try
            {
                if (null == section)
                {
                    throw new XmlException(Resources.ConfiguredList_NullSectionMessage);
                }

                foreach (XmlNode list in section.ChildNodes)
                {
                    if (null == list)
                    {
                        continue;
                    }

                    if (null == list.Attributes)
                    {
                        continue;
                    }

                    var name = list.Attributes["name"].Value;
                    result.Add(name, LoadItems(list));
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException(exception.Message, exception, section);
            }

            return result;
        }

        private static IEnumerable<string> LoadItems(XmlNode list)
        {
            var result = new List<string>();

            var items = list.SelectNodes("item");
            if (null != items)
            {
                result.AddRange(from XmlNode item in items
                                where null != item
                                select item.InnerText);
            }

            return result;
        }
    }
}