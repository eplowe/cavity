namespace Cavity.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.IO;
    using Cavity.Models;

    public static class PackageFile
    {
        private const string _empty =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "{0}" +
            "<packages>" +
            "</packages>";

        private static IXPathNavigable EmptyTemplate
        {
            get
            {
                var value = new XmlDocument();
                value.LoadXml(_empty.FormatWith(Environment.NewLine));

                return value;
            }
        }

        public static void Create(Project project)
        {
            Create(project, new List<Package>());
        }

        public static void Create(Project project,
                                  Package package)
        {
            var packages = new List<Package>
                               {
                                   package
                               };
            Create(project, packages);
        }

        public static void Create(Project project,
                                  IEnumerable<Package> packages)
        {
            var xml = EmptyTemplate;
            var navigator = xml.CreateNavigator();
            navigator.MoveToChild("packages", string.Empty);

            foreach (var package in packages)
            {
                using (var writer = navigator.AppendChild())
                {
                    writer.WriteStartElement("package");
                    writer.WriteAttributeString("id", package.Id);
                    writer.WriteAttributeString("version", package.Version);
                }
            }

            project.Location.Directory.ToFile("packages.config").Create(xml);
        }
    }
}