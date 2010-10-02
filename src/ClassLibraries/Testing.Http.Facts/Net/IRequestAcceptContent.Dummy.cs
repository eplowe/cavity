namespace Cavity.Net
{
    using System;

    public sealed class IRequestAcceptContentDummy : IRequestAcceptContent
    {
        IRequestAcceptLanguage IRequestAcceptContent.Accept(string value)
        {
            throw new NotSupportedException();
        }

        IRequestAcceptLanguage IRequestAcceptContent.AcceptAnyContent()
        {
            throw new NotSupportedException();
        }
    }
}