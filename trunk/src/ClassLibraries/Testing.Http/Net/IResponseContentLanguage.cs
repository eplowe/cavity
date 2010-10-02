namespace Cavity.Net
{
    using System.Globalization;

    public interface IResponseContentLanguage
    {
        IResponseContentMD5 HasContentLanguage(CultureInfo language);

        IResponseContentMD5 HasContentLanguage(string language);

        IResponseContentMD5 HasContentLanguageOfEnglish();

        IResponseContentMD5 IgnoreContentLanguage();
    }
}