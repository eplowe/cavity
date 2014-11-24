namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.XPath;

    public sealed class Project
    {
        private Project()
        {
        }

        public FileInfo Location { get; private set; }

        public IEnumerable<Package> Packages
        {
            get
            {
                // ReSharper disable PossibleNullReferenceException
                var config = new FileInfo(Path.Combine(Location.Directory.FullName, "packages.config"));
                if (!config.Exists)
                {
                    yield break;
                }

                // ReSharper restore PossibleNullReferenceException
                var xml = new XmlDocument();
                xml.Load(config.FullName);
                var packages = xml.SelectNodes("/packages/package");
                if (null != packages)
                {
                    foreach (XmlNode package in packages)
                    {
                        if (null != package.Attributes)
                        {
                            var id = package.Attributes["id"];
                            var version = package.Attributes["version"];
                            if (null == (id ?? version))
                            {
                                continue;
                            }

                            yield return new Package
                                             {
                                                 Id = null == id ? null : id.Value,
                                                 Version = null == version ? null : version.Value
                                             };
                        }
                    }
                }
            }
        }

        public IEnumerable<Reference> References
        {
            get
            {
                var xml = new XmlDocument();
                xml.Load(Location.FullName);
                var namespaces = ToNamespaces(xml.NameTable);
                var references = xml.SelectNodes("//b:Reference", namespaces);
                if (null != references)
                {
                    foreach (XmlNode reference in references)
                    {
                        if (null != reference.Attributes)
                        {
                            var include = reference.Attributes["Include"];
                            if (null != include)
                            {
                                var hint = reference.SelectSingleNode("b:HintPath", namespaces);

                                yield return new Reference
                                                 {
                                                     Include = include.Value,
                                                     Hint = null == hint ? null : hint.InnerText
                                                 };
                            }
                        }
                    }
                }
            }
        }

        public IXPathNavigable Xml
        {
            get
            {
                var value = new XmlDocument();
                value.Load(Location.FullName);

                return value;
            }
        }

        public static Project Load(FileInfo location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            if (!location.Exists)
            {
                throw new FileNotFoundException(location.FullName);
            }

            return new Project
                       {
                           Location = location
                       };
        }

        public static XmlNamespaceManager ToNamespaces(XPathNavigator navigator)
        {
            if (null == navigator)
            {
                throw new ArgumentNullException("navigator");
            }

            return ToNamespaces(navigator.NameTable);
        }

        public static XmlNamespaceManager ToNamespaces(XmlNameTable nameTable)
        {
            var namespaces = new XmlNamespaceManager(nameTable);
            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");

            return namespaces;
        }

        public Package PackageReference(Package package)
        {
            if (null == package)
            {
                throw new ArgumentNullException("package");
            }

            var exact = string.Concat(@"\packages\", package.ToString(), '\\');
            foreach (var reference in References)
            {
                if (null == reference.Hint)
                {
                    continue;
                }

                var index = reference.Hint.IndexOf(exact, StringComparison.OrdinalIgnoreCase);
                if (-1 == index)
                {
                    continue;
                }

                return package;
            }

            var inexact = string.Concat(@"\packages\", package.Id, '.');
            foreach (var reference in References)
            {
                if (null == reference.Hint)
                {
                    continue;
                }

                var index = reference.Hint.IndexOf(inexact, StringComparison.OrdinalIgnoreCase);
                if (-1 == index)
                {
                    continue;
                }

                var version = reference.Hint.Substring(index + inexact.Length).Split('\\')[0];
                return new Package(package.Id, version);
            }

            return null;
        }
    }
}