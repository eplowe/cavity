namespace Cavity.Data
{
    using System.Xml.XPath;

    public interface IRepository
    {
        bool Delete(AbsoluteUri urn);

        bool Delete(AlphaDecimal urn);

        bool Exists(AbsoluteUri urn);

        bool Exists(AlphaDecimal urn);

        bool Exists(XPathExpression xpath);
    }
}