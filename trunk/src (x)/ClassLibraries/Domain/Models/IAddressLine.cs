namespace Cavity.Models
{
    public interface IAddressLine
    {
#if NET40
        dynamic Data { get; }
#else
        object Data { get; }
#endif

        string ToString(IFormatAddress renderer);
    }
}