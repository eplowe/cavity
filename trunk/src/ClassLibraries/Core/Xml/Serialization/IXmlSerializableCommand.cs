namespace Cavity.Xml.Serialization
{
    using System.Xml.Serialization;

    public interface IXmlSerializableCommand : ICommand, IXmlSerializable
    {
    }
}