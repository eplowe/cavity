namespace Cavity.Net
{
    public interface IRequestAcceptContent
    {
        IRequestAcceptLanguage Accept(string value);

        IRequestAcceptLanguage AcceptAnyContent();
    }
}