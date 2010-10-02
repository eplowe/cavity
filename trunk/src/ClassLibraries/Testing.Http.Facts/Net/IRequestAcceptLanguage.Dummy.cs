namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class IRequestAcceptLanguageDummy : IRequestAcceptLanguage
    {
        IRequestMethod IRequestAcceptLanguage.AcceptAnyLanguage()
        {
            throw new NotSupportedException();
        }

        IRequestMethod IRequestAcceptLanguage.AcceptLanguage(CultureInfo language)
        {
            throw new NotSupportedException();
        }

        IRequestMethod IRequestAcceptLanguage.AcceptLanguage(string language)
        {
            throw new NotSupportedException();
        }
    }
}