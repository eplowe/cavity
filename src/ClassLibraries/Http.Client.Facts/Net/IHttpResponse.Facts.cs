namespace Cavity.Net
{
    using System;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class IHttpResponseFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpResponse).IsInterface);
        }

        [Fact]
        public void IHttpResponse_StatusLine_get()
        {
            try
            {
                StatusLine value = (new IHttpResponseDummy() as IHttpResponse).StatusLine;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpMessage_Body_get()
        {
            try
            {
                IContent value = (new IHttpResponseDummy() as IHttpMessage).Body;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpMessage_Headers_get()
        {
            try
            {
                IHttpHeaderCollection value = (new IHttpResponseDummy() as IHttpMessage).Headers;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}