namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Net;
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
    public sealed class XmlUriAttribute : DataAttribute
    {
        public XmlUriAttribute(params string[] locations)
            : this()
        {
            if (null == locations)
            {
                throw new ArgumentNullException("locations");
            }

            if (0 == locations.Length)
            {
                throw new ArgumentOutOfRangeException("locations");
            }

            Locations = locations;
        }

        private XmlUriAttribute()
        {
        }

        public IEnumerable<string> Locations { get; private set; }

        public static FileInfo Download(AbsoluteUri location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            FileInfo file = null;

            var request = WebRequest.Create((Uri)location);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (null != stream)
                    {
                        using (var reader = new StreamReader(stream))
                        {
#if NET20
                            file = new FileInfo(StringExtensionMethods.FormatWith("{0}.xml", AlphaDecimal.Random()));
                            FileInfoExtensionMethods.Create(file, reader.ReadToEnd());
#else
                            file = new FileInfo("{0}.xml".FormatWith(AlphaDecimal.Random()));
                            file.Create(reader.ReadToEnd());
#endif
                        }
                    }
                }
            }

            return file;
        }

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
            if (Cavity.Collections.IEnumerableExtensionMethods.Count(Locations) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, Cavity.Collections.IEnumerableExtensionMethods.Count(Locations), parameterTypes.Length));
            }
#else
            if (Locations.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Locations.Count(), parameterTypes.Length));
            }
#endif

            var list = new List<object>();
            var index = -1;
            foreach (var location in Locations)
            {
                var file = Download(location);
                index++;
                if (parameterTypes[index] == typeof(XmlDocument) || parameterTypes[index] == typeof(IXPathNavigable))
                {
                    var xml = new XmlDocument();
                    xml.Load(file.FullName);

                    list.Add(xml);
                    continue;
                }

                if (parameterTypes[index] == typeof(XPathNavigator))
                {
                    var xml = new XmlDocument();
                    xml.Load(file.FullName);

                    list.Add(xml.CreateNavigator());
                    continue;
                }
                
#if !NET20
                if (parameterTypes[index] == typeof(XDocument))
                {
                    list.Add(XDocument.Load(file.FullName));
                    continue;
                }
#endif
                
#if NET20
                list.Add(StringExtensionMethods.XmlDeserialize(FileInfoExtensionMethods.ReadToEnd(file), parameterTypes[index]));
#else
                list.Add(file.ReadToEnd().XmlDeserialize(parameterTypes[index]));
#endif
            }

            yield return list.ToArray();
        }
    }
}