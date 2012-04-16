namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;
    using System.Xml;
#if !NET20
    using System.Xml.Linq;
#endif
    using System.Xml.XPath;

    using Cavity.IO;
    using Cavity.Properties;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class XmlFileAttribute : DataAttribute
    {
        public XmlFileAttribute(params string[] files)
            : this()
        {
            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

            if (0 == files.Length)
            {
                throw new ArgumentOutOfRangeException("files");
            }

            Files = files;
        }

        private XmlFileAttribute()
        {
        }

        public IEnumerable<string> Files { get; private set; }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, 
                                                      Type[] parameterTypes)
        {
            if (null == methodUnderTest)
            {
                throw new ArgumentNullException("methodUnderTest");
            }

            if (null == parameterTypes)
            {
                throw new ArgumentNullException("parameterTypes");
            }

#if NET20
            if (Cavity.Collections.IEnumerableExtensionMethods.Count(Files) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, Cavity.Collections.IEnumerableExtensionMethods.Count(Files), parameterTypes.Length));
            }
#else
            if (Files.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Files.Count(), parameterTypes.Length));
            }
#endif

            var list = new List<object>();
            var index = -1;
            foreach (var file in Files)
            {
                var info = new FileInfo(file);
                index++;
                if (parameterTypes[index] == typeof(XmlDocument) || parameterTypes[index] == typeof(IXPathNavigable))
                {
                    var xml = new XmlDocument();
                    xml.Load(info.FullName);

                    list.Add(xml);
                    continue;
                }

                if (parameterTypes[index] == typeof(XPathNavigator))
                {
                    var xml = new XmlDocument();
                    xml.Load(info.FullName);

                    list.Add(xml.CreateNavigator());
                    continue;
                }
                
#if !NET20
                if (parameterTypes[index] == typeof(XDocument))
                {
                    list.Add(XDocument.Load(info.FullName));
                    continue;
                }
#endif
                
#if NET20
                list.Add(StringExtensionMethods.XmlDeserialize(FileInfoExtensionMethods.ReadToEnd(info), parameterTypes[index]));
#else
                list.Add(info.ReadToEnd().XmlDeserialize(parameterTypes[index]));
#endif
            }

            yield return list.ToArray();
        }
    }
}