namespace Cavity.Net
{
    using System;
    using Cavity.Xml;

    public sealed class IResponseXmlDummy : ITestHttpDummy, IResponseXml
    {
        IResponseXml IResponseXml.Evaluate<T>(T expected, params string[] xpaths)
        {
            throw new NotSupportedException();
        }

        IResponseXml IResponseXml.Evaluate<T>(T expected, string xpath, params XmlNamespace[] namespaces)
        {
            throw new NotSupportedException();
        }

        IResponseXml IResponseXml.EvaluateFalse(params string[] xpaths)
        {
            throw new NotSupportedException();
        }

        IResponseXml IResponseXml.EvaluateTrue(params string[] xpaths)
        {
            throw new NotSupportedException();
        }
    }
}