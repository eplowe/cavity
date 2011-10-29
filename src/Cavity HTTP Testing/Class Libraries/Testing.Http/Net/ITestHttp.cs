namespace Cavity.Net
{
    public interface ITestHttp
    {
        string Location { get; }

        bool Result { get; }

        ITestHttp HasContentLocation(AbsoluteUri location);
    }
}