namespace Cavity.Net
{
    public interface IResponseContentMD5
    {
        IResponseContent HasContentMD5();

        IResponseContent IgnoreContentMD5();
    }
}