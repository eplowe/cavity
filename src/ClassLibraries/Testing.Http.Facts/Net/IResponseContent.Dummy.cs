namespace Cavity.Net
{
    using System;
    using System.Net.Mime;

    public sealed class IResponseContentDummy : IResponseContent
    {
        ITestHttp IResponseContent.ResponseHasNoContent()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIs(ContentType type)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsApplicationJson()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsApplicationJson(ContentType type)
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseContent.ResponseIsApplicationXhtml()
        {
            throw new NotSupportedException();
        }

        IResponseXml IResponseContent.ResponseIsApplicationXml()
        {
            throw new NotSupportedException();
        }

        IResponseXml IResponseContent.ResponseIsApplicationXml(ContentType type)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsImageIcon()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsTextCss()
        {
            throw new NotSupportedException();
        }

        IResponseHtml IResponseContent.ResponseIsTextHtml()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsTextJavaScript()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseContent.ResponseIsTextPlain()
        {
            throw new NotSupportedException();
        }
    }
}