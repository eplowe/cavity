namespace Cavity
{
    using System.Net;

    public interface IHttpExpectation
    {
        bool Verify(CookieContainer cookies);
    }
}