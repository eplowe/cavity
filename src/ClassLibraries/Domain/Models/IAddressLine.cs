namespace Cavity.Models
{
    public interface IAddressLine
    {
        string Original { get; }

#if NET40
        dynamic Value { get; }
#else
        object Value { get; }
#endif
    }
}