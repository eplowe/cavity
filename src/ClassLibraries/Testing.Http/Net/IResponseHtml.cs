namespace Cavity.Net
{
    public interface IResponseHtml : ITestHttp
    {
        IResponseHtml Evaluate<T>(T expected,
                                  params string[] xpaths);

        IResponseHtml EvaluateFalse(params string[] xpaths);

        IResponseHtml EvaluateTrue(params string[] xpaths);

        IResponseHtml HasRobotsTag(string value);

        IResponseHtml HasStyleSheetLink(string href);
    }
}