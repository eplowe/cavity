namespace Cavity.Testing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.XPath;

    using Cavity.IO;
    using Cavity.Models;

    public static class ProjectFile
    {
        private const string _empty =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "{0}" +
            "<Project ToolsVersion='4.0' DefaultTargets='Build' xmlns='http://schemas.microsoft.com/developer/msbuild/2003'>" +
            "<ItemGroup>" +
            "</ItemGroup>" +
            "</Project>";

        private const string _xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";

        private static IXPathNavigable EmptyTemplate
        {
            get
            {
                var value = new XmlDocument();
                value.LoadXml(_empty.FormatWith(Environment.NewLine));

                return value;
            }
        }

        public static Project Create(FileInfo location)
        {
            return Create(location, new List<Reference>());
        }

        public static Project Create(FileInfo location,
                                     Reference reference)
        {
            var references = new List<Reference>
                               {
                                   reference
                               };
            return Create(location, references);
        }

        public static Project Create(FileInfo location,
                                     IEnumerable<Reference> references)
        {
            var xml = EmptyTemplate;
            var navigator = xml.CreateNavigator();
            navigator.MoveToChild("Project", _xmlns);
            navigator.MoveToChild("ItemGroup", _xmlns);

            foreach (var reference in references)
            {
                using (var child = navigator.AppendChild())
                {
                    child.WriteStartElement("Reference");
                    child.WriteAttributeString("Include", reference.Include);
                    if (null != reference.Hint)
                    {
                        child.WriteElementString("HintPath", reference.Hint);
                    }
                }
            }

            return Project.Load(location.Create(xml));
        }
    }
}