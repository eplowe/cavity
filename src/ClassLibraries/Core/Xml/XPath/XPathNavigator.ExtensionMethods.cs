namespace Cavity.Xml.XPath
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.XPath;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "XPath", Justification = "Following the Microsoft naming convention.")]
    public static class XPathNavigatorExtensionMethods
    {
#if NET20
        public static T Evaluate<T>(XPathNavigator obj,
                                    string xpath)
#else
        public static T Evaluate<T>(this XPathNavigator obj,
                                    string xpath)
#endif
        {
            return Evaluate<T>(obj, xpath, null);
        }

#if NET20
        public static T Evaluate<T>(XPathNavigator obj,
                                    string xpath,
                                    IXmlNamespaceResolver namespaces)
#else
        public static T Evaluate<T>(this XPathNavigator obj,
                                    string xpath,
                                    IXmlNamespaceResolver namespaces)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == xpath)
            {
                throw new ArgumentNullException("xpath");
            }

            return null == namespaces
                       ? (T)obj.Evaluate(xpath)
                       : (T)obj.Evaluate(xpath, namespaces);
        }
    }
}