namespace Cavity.Net
{
    using Cavity.Data;

    public interface IWebRequest
    {
        DataCollection Headers { get; }

        string Method { get; set; }

        AbsoluteUri RequestUri { get; set; }
    }
}