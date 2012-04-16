namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;
    using System.Xml;
#if !NET20
    using System.Xml.Linq;
#endif
    using System.Xml.XPath;

    using Cavity.Properties;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class XmlDataAttribute : DataAttribute
    {
        public XmlDataAttribute(params string[] values)
            : this()
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (0 == values.Length)
            {
                throw new ArgumentOutOfRangeException("values");
            }

            Values = values;
        }

        private XmlDataAttribute()
        {
        }

        public IEnumerable<string> Values { get; private set; }

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
            if (Cavity.Collections.IEnumerableExtensionMethods.Count(Values) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, Cavity.Collections.IEnumerableExtensionMethods.Count(Values), parameterTypes.Length));
            }
#else
            if (Values.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Values.Count(), parameterTypes.Length));
            }
#endif

            var list = new List<object>();
            var index = -1;
            foreach (var value in Values)
            {
                index++;
                if (parameterTypes[index] == typeof(XmlDocument) || parameterTypes[index] == typeof(IXPathNavigable))
                {
                    var xml = new XmlDocument();
                    xml.LoadXml(value);

                    list.Add(xml);
                    continue;
                }

                if (parameterTypes[index] == typeof(XPathNavigator))
                {
                    var xml = new XmlDocument();
                    xml.LoadXml(value);

                    list.Add(xml.CreateNavigator());
                    continue;
                }
                
#if !NET20
                if (parameterTypes[index] == typeof(XDocument))
                {
                    list.Add(XDocument.Parse(value));
                    continue;
                }
#endif

#if NET20
                list.Add(StringExtensionMethods.XmlDeserialize(value, parameterTypes[index]));
#else
                list.Add(value.XmlDeserialize(parameterTypes[index]));
#endif
            }

            yield return list.ToArray();
        }
    }
}