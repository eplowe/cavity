namespace Cavity.Net
{
    using System.Globalization;

    public interface IRequestAcceptLanguage
    {
        IRequestMethod AcceptAnyLanguage();

        IRequestMethod AcceptLanguage(CultureInfo language);

        IRequestMethod AcceptLanguage(string language);
    }
}