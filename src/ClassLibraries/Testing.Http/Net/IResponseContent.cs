namespace Cavity.Net
{
    using System.Net.Mime;

    public interface IResponseContent
    {
        ITestHttp ResponseHasNoContent();

        ITestHttp ResponseIs(ContentType type);

        ITestHttp ResponseIsApplicationJson();

        ITestHttp ResponseIsApplicationJson(ContentType type);

        IResponseHtml ResponseIsApplicationXhtml();

        IResponseXml ResponseIsApplicationXml();

        IResponseXml ResponseIsApplicationXml(ContentType type);

        ITestHttp ResponseIsImageIcon();

        ITestHttp ResponseIsTextCss();

        IResponseHtml ResponseIsTextHtml();

        ITestHttp ResponseIsTextJavaScript();

        ITestHttp ResponseIsTextPlain();
    }
}