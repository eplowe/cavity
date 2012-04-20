namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Xml.XPath;

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
                            var file = new FileInfo("{0}.html".FormatWith(AlphaDecimal.Random()));
                            file.Create(reader.ReadToEnd());

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

            if (Locations.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Locations.Count(), parameterTypes.Length));
            }

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
                    list.Add(Download(location).TabularData());
                    continue;
                }

                throw new InvalidOperationException(Resources.HtmlAttribute_UnsupportedParameterType);
            }

            yield return list.ToArray();
        }
    }
}