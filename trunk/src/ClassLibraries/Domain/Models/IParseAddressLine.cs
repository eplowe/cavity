namespace Cavity.Models
{
    public interface IParseAddressLine
    {
        IAddressLine FromString(string expression);
    }
}