namespace Cavity.Net
{
    public interface ITestHttpExpectation
    {
        bool Check(Response response);
    }
}