namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;
    using System.Xml.XPath;
#if NET20
    using Cavity.Collections;
#endif
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

#if NET20
            if (IEnumerableExtensionMethods.Count(Files) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, IEnumerableExtensionMethods.Count(Files), parameterTypes.Length));
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
#if NET20
                    list.Add(HtmlDocumentExtensionMethods.TabularData(html));
#else
                    list.Add(html.TabularData());
#endif
                    continue;
                }

                throw new InvalidOperationException(Resources.HtmlAttribute_UnsupportedParameterType);
            }

            yield return list.ToArray();
        }
    }
}