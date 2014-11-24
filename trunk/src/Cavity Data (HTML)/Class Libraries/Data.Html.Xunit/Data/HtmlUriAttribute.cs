namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Net;
    using System.Reflection;
    using System.Xml.XPath;
#if NET20
    using Cavity.Collections;
#endif
    using Cavity.IO;
    using Cavity.Properties;
    using HtmlAgilityPack;
    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class HtmlUriAttribute : DataAttribute
    {
        public HtmlUriAttribute(params string[] locations)
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

        private HtmlUriAttribute()
        {
        }

        public IEnumerable<string> Locations { get; private set; }

        public static HtmlDocument Download(AbsoluteUri location)
        {
            if (null == location)
            {
                throw new ArgumentNullException("location");
            }

            HtmlDocument html = null;

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
                            var file = new FileInfo(StringExtensionMethods.FormatWith("{0}.html", AlphaDecimal.Random()));
                            FileInfoExtensionMethods.Create(file, reader.ReadToEnd());
#else
                            var file = new FileInfo("{0}.html".FormatWith(AlphaDecimal.Random()));
                            file.Create(reader.ReadToEnd());
#endif

                            html = new HtmlDocument();
                            html.Load(file.FullName);
                        }
                    }
                }
            }

            return html;
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
            if (IEnumerableExtensionMethods.Count(Locations) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, IEnumerableExtensionMethods.Count(Locations), parameterTypes.Length));
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
                index++;
                if (parameterTypes[index] == typeof(HtmlDocument) ||
                    parameterTypes[index] == typeof(IXPathNavigable))
                {
                    list.Add(Download(location));
                    continue;
                }

                if (parameterTypes[index] == typeof(XPathNavigator))
                {
                    list.Add(Download(location).CreateNavigator());
                    continue;
                }

                if (parameterTypes[index] == typeof(DataSet))
                {
#if NET20
                    list.Add(HtmlDocumentExtensionMethods.TabularData(Download(location)));
#else
                    list.Add(Download(location).TabularData());
#endif
                    continue;
                }

                throw new InvalidOperationException(Resources.HtmlAttribute_UnsupportedParameterType);
            }

            yield return list.ToArray();
        }
    }
}