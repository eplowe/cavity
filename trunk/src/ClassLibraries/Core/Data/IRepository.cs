namespace Cavity.Data
{
    using System.Xml.XPath;

    public interface IRepository
    {
        bool Delete(AbsoluteUri urn);

        bool Delete(AlphaDecimal key);

        bool Exists(AbsoluteUri urn);

        bool Exists(AlphaDecimal key);

        bool Exists(XPathExpression xpath);
    }
}