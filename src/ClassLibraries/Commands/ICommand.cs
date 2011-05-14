namespace Cavity
{
    using System.Xml.Serialization;

    public interface ICommand : IXmlSerializable
    {
        bool Act();

        bool Revert();
    }
}