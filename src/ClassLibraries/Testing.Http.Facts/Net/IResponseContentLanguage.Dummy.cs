namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class IResponseContentLanguageDummy : IResponseContentLanguage
    {
        IResponseContentMD5 IResponseContentLanguage.HasContentLanguage(CultureInfo language)
        {
            throw new NotSupportedException();
        }

        IResponseContentMD5 IResponseContentLanguage.HasContentLanguage(string language)
        {
            throw new NotSupportedException();
        }

        IResponseContentMD5 IResponseContentLanguage.HasContentLanguageOfEnglish()
        {
            throw new NotSupportedException();
        }

        IResponseContentMD5 IResponseContentLanguage.IgnoreContentLanguage()
        {
            throw new NotSupportedException();
        }
    }
}