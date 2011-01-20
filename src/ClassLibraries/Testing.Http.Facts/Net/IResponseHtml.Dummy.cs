namespace Cavity.Net
{
    using System;

    public sealed class IResponseHtmlDummy : ITestHttpDummy, IResponseHtml
    {
        IResponseHtml IResponseHtml.Evaluate<T>(T expected,
                                                params string[] xpaths)
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseHtml.EvaluateFalse(params string[] xpaths)
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseHtml.EvaluateTrue(params string[] xpaths)
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseHtml.HasRobotsTag(string value)
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseHtml.HasStyleSheetLink(string href)
        {
            throw new NotSupportedException();
        }
    }
}