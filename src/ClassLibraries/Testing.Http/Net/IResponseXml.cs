namespace Cavity.Net
{
    using Cavity.Xml;

    public interface IResponseXml : ITestHttp
    {
        IResponseXml Evaluate<T>(T expected, params string[] xpaths);

        IResponseXml Evaluate<T>(T expected, string xpath, params XmlNamespace[] namespaces);

        IResponseXml EvaluateFalse(params string[] xpaths);

        IResponseXml EvaluateTrue(params string[] xpaths);
    }
}