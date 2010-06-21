namespace Cavity.Net
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class IHttpRequestFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpRequest).IsInterface);
        }

        [Fact]
        public void is_IHttpMessage()
        {
            Assert.True(typeof(IHttpRequest).Implements(typeof(IHttpMessage)));
        }

        [Fact]
        public void IHttpRequest_AbsoluteUri_get()
        {
            try
            {
                Uri value = (new IHttpRequestDummy() as IHttpRequest).AbsoluteUri;
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
                IHttpBody value = (new IHttpRequestDummy() as IHttpMessage).Body;
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
                IHttpHeaderCollection value = (new IHttpRequestDummy() as IHttpMessage).Headers;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpRequest_Write_StreamWriter()
        {
            try
            {
                (new IHttpRequestDummy() as IHttpRequest).Write(null as StreamWriter);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}