namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.XPath;

    using Cavity.Properties;

    using HtmlAgilityPack;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class HtmlFileAttribute : DataAttribute
    {
        public HtmlFileAttribute(params string[] files)
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

        private HtmlFileAttribute()
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

            if (Files.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Files.Count(), parameterTypes.Length));
            }

            var list = new List<object>();
            var index = -1;
            foreach (var file in Files)
            {
                var info = new FileInfo(file);
                index++;
                if (parameterTypes[index] == typeof(HtmlDocument) || parameterTypes[index] == typeof(IXPathNavigable))
                {
                    var html = new HtmlDocument();
                    html.Load(info.FullName);

                    list.Add(html);
                    continue;
                }

                if (parameterTypes[index] == typeof(XPathNavigator))
                {
                    var html = new HtmlDocument();
                    html.Load(info.FullName);

                    list.Add(html.CreateNavigator());
                    continue;
                }

                if (parameterTypes[index] == typeof(DataSet))
                {
                    var html = new HtmlDocument();
                    html.Load(info.FullName);

                    list.Add(html.TabularData());
                    continue;
                }

                throw new InvalidOperationException(Resources.HtmlAttribute_UnsupportedParameterType);
            }

            yield return list.ToArray();
        }
    }
}